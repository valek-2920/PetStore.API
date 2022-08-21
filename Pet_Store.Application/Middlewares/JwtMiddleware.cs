using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.IdentityModel.Tokens;
using Pet_Store.Application.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pet_Store.Application.Middlewares
{
    public class JwtMiddleware
    {
        public JwtMiddleware(IJwtManager jwtManager, RequestDelegate next)
        {
            _jwtManager = jwtManager;
            _next = next;
        }

        readonly IJwtManager _jwtManager;
        readonly RequestDelegate _next;

        public static bool TryGetToken(HttpRequest request, out string token)
        {
            token = null;

            HttpRequestMessage requestMessage =
                new HttpRequestMessageFeature(request.HttpContext).HttpRequestMessage;

            IEnumerable<string> authorizationHeaders;
            if (!requestMessage.Headers.TryGetValues("Authorization", out authorizationHeaders) ||
                authorizationHeaders.Count() > 1)
            {
                return false;
            }

            var bearer = authorizationHeaders.FirstOrDefault();

            token =
                bearer.StartsWith("Bearer ")
                    ? bearer.Substring(7)
                    : bearer;

            return true;
        }

        public async Task Invoke(HttpContext context)
        {
            bool success =
                context.GetEndpoint().Metadata.OfType<AllowAnonymousAttribute>().Any();

            if (!success)
            {
                if (TryGetToken(context.Request, out string bearer))
                {
                    if (_jwtManager.IsTokenValid(bearer, out SecurityToken authorizedToken))
                    {
                        var jwtToken = (JwtSecurityToken)authorizedToken;
                        var userName =
                            jwtToken.Claims.FirstOrDefault
                                (s => s.Type.Equals("unique_name", StringComparison.OrdinalIgnoreCase)).Value;

                        var claims = new[] { new Claim("name", userName) };
                        var identity = new ClaimsIdentity(claims, "basic");

                        context.User = new ClaimsPrincipal(identity);
                        success = true;
                    }
                }
            }

            if (!success)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var result = JsonSerializer.Serialize(new { message = "Unauthorized" });
                await response.WriteAsync(result);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
