using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Pet_Store.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Application.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {


        public PermissionHandler
            (RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<IdentityUser> _userManager;


        async Task<List<Claim>> ListClaimsAsync(AuthorizationHandlerContext context)
        {
            var identity = (ClaimsIdentity)context.User.Identity;
            var user = await _userManager.FindByEmailAsync
                    (identity.Claims.FirstOrDefault
                      (s => s.Type.EndsWith
                          ("emailaddress", StringComparison.OrdinalIgnoreCase)).Value);

            List<Claim> claims = new List<Claim>();
            foreach (var role in _roleManager.Roles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    foreach (var claim in await _roleManager.GetClaimsAsync(role))
                    {
                        if (!claims.Contains(claim))
                        {
                            claims.Add(claim);
                        }
                    }
                }
            }

            return claims;
        }



        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context == null)
            {
                context.Fail();
            }
            else
            {
                var claims = ListClaimsAsync(context).GetAwaiter().GetResult();
                var description =
                    requirement.PermissionType.GetDescription().Split(':');
                var claim =
                    new Claim(description.FirstOrDefault(), description.LastOrDefault());

                if (claims.Exists(e => e.Value.Contains(claim.Value)))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

            }

            return Task.CompletedTask;

        }
    }
}
