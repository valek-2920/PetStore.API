using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.ViewModels;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pet_Store.Application.Handlers;

namespace Pet_Store.Responsive.Controllers
{
    public class InventarioController : Controller
    {

        readonly IConfiguration _configuration;
        readonly IInventarioServices _services;
        readonly IWebHostEnvironment _hostEnvironment;

        public InventarioController
            (
            IConfiguration configuration,
            IInventarioServices services,
            IWebHostEnvironment hostEnvironment
            )
        {
            _services = services;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Inventario()
        {
            var products = await _services.getProductsAsync();

            return View(products);

        }

       
        [HttpGet]
         [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Upsert(int? id)
        {
            var categories = await _services.GetCategoriesAsync();

            ProductsViewModel viewModel = new()
            {
                Product = new(),

                CategoryList = categories.Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.CategoryId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //insert new product
                return View(viewModel);
            }
            else 
            {
                //update existing product
                viewModel.Product = await _services.getProductById( (int) id);
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Upsert([FromForm] ProductsViewModel model, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\productos");
                    var extension = Path.GetExtension(file.FileName);

                    if (model.Product.Files != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, model.Product.Files.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    model.Product.Files = @"\img\productos\" + fileName + extension;

                }
                if (model.Product.ProductId == 0)
                {
                    //add
                    await _services.addProductAsync(model.Product);
                }
                else
                {
                    //update
                    await _services.updateProductById(model.Product);

                }
              return RedirectToAction("Inventario");

            }
            return View(model);
        }

        [HttpDelete]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> EliminarProductos(int id)
        {

            var model = await _services.getProductById(id);

                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, model.Files.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                await _services.deleteProductById(id);
                return RedirectToAction("Inventario");
        }

        [HttpGet]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> Categorias()
        {
            var categories = await _services.GetCategoriesAsync();
            return View(categories);
        }

        public IActionResult AgregarCategorias()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> AgregarCategorias(Category category)
        {
            await _services.AddCategoryAsync(category);
            return RedirectToAction("Categorias");
        }

        [HttpDelete]
        [Authorize(Policy = PermissionTypeNames.MANAGEROLES)]
        public async Task<IActionResult> EliminarCategorias(int id)
        {
            await _services.deleteCategoryById(id);
            return RedirectToAction("Categorias");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
