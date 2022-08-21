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

        public async Task<IActionResult> Inventario()
        {
            var products = await _services.getProductsAsync();

            return View(products);

        }

        [HttpGet]
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
        public async Task<IActionResult> Upsert(ProductsViewModel model, IFormFile? file)
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
        public async Task<IActionResult> EliminarProductos(int id)
        {
            await _services.deleteProductById(id);
            return RedirectToAction("Inventario");

        }

        [HttpGet]
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
        public async Task<IActionResult> AgregarCategorias(Category category)
        {
            await _services.AddCategoryAsync(category);
            return RedirectToAction("Categorias");
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCategorias(int id)
        {
            var response = await _services.deleteCategoryById(id);
            ViewBag.response = response;
            return RedirectToAction("Categorias");

        }

    }
}
