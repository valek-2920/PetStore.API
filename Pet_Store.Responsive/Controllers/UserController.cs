using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pet_Store.Application.Handlers;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.Entities;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Responsive.Services.IServices;
using PetStore.Infraestructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Store.Responsive.Controllers
{
    public class UserController : Controller
    {
        public UserController
            (
                RoleManager<IdentityRole> roleManager,
                UserManager<IdentityUser> userManager,
                IApplicationDbContext context,
                IUserServices services
                
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _services = services;
        }

        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<IdentityUser> _userManager;
        IApplicationDbContext _context;
        IUserServices _services;

        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public IActionResult Clientes()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {

            var users = await _services.getUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            Users model = new();
           
            if (id == null || id == 0)
            {
                //insert new product
                return View(model);
            }
            else
            {
                //update existing product
                model = await _services.getUserById( (int) id);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Users model)
        {

            if (ModelState.IsValid)
            {

                if (model.Id == null)
                {
                    //add
                    await _services.addUserAsync(model);
                }
                else
                {
                    //update
                    await _services.updateUserById(model);

                }
                return RedirectToAction("Inventario");

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuarios(int id)
        {
            var response = await _services.deleteUserById(id);
            ViewBag.response = response;
            return RedirectToAction("Clientes");

        }


        public IActionResult Roles()
        {
            RoleViewModel model =
                new RoleViewModel
                {
                    Roles =
                        _roleManager.Roles.Select
                            (s => new Role { Id = s.Id, Name = s.Name })
                };

            return View(model);
        }
        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Upsert(string id = null)
        {
            Role role = new Role();

            if (!string.IsNullOrEmpty(id))
            {
                IdentityRole identityRole = await _roleManager.FindByIdAsync(id);

                if (identityRole == null)
                {
                    return NotFound();
                }

                role.Id = identityRole.Id;
                role.Name = identityRole.Name;
            }

            return View(role);
        }

        [HttpPost]
        [Route("[action]")]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Upsert(Role model, string id = null)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();

                if (!string.IsNullOrEmpty(model.Id))
                {
                    role = await _roleManager.FindByIdAsync(model.Id);
                }

                role.Name = model.Name;

                var result =
                    !string.IsNullOrEmpty(model.Id)
                        ? await _roleManager.UpdateAsync(role)
                        : await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "User");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> UsersRole(string id)
        {
            
            UserRoleViewModel model =
                new UserRoleViewModel
                {
                    RoleId = id
                };

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var user in _userManager.Users)
            {
                UserRole output =
                    new UserRole
                    {
                        Email = user.Email,
                        Selected =
                            await _userManager.IsInRoleAsync(user, role.Name)
                    };
                model.Users.Add(output);
            }

            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> UsersRole(UserRoleViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var input in model.Users)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                IdentityResult result = null;

                if (input.Selected && !await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!input.Selected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                if (result != null && !result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return RedirectToAction("Roles", "User");
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return Json(new { success = false, message = $"No se encuentra el rol con id {id}" });
            }

            var result = await _roleManager.DeleteAsync(role);

            return Json(new { success = true, message = "Rol borrado exitosamente." });

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
