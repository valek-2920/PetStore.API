using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.ViewModels;
using PetStore.DataAccess.Repository.UnityOfWork;
using PetStore.Domain.Models.ViewModels;
using Project_PetStore.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IUnityOfWork _unityOfWork;

        public ProductsController(IUnityOfWork unityOfWork, ApplicationDbContext context)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> CreateProduct([FromForm] NewProduct model)
        {
            try
            {
                var product = await _unityOfWork.ProductsRepository.CreateProduct(model);

                if (product != null)
                {
                    return Ok(product);
                }

                 return BadRequest("Por favor revisa los detalles del producto");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest("Se ha dado un error: " + msg);
            }
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            var allProducts = await _unityOfWork.ProductsRepository.GetAllProductsAsync();

            if (allProducts != null)
            {
                var domainProjects = new List<Products>();

                foreach (var items in allProducts)
                {
                    domainProjects.Add(new Products()
                    {
                        ProductId = items.ProductId,
                        Name = items.Name,
                        Description = items.Description,
                        Files = new Files()
                        {
                            Id = items.Files.Id,
                            Name = items.Files.Name,
                            Size = items.Files.Size,
                            Url = items.Files.Url,
                            uploadDateTime = items.Files.uploadDateTime
                        },
                        Category = new Category()
                        {
                            CategoryId = items.Category.CategoryId,
                            Description = items.Category.Description
                        }
                    });
                }
                return Ok(domainProjects);
            }
            return BadRequest("No se encuentran productos en el sistema.");
        }

        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unityOfWork.ProductsRepository.GetProductAsync(id);

            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("El producto solicitado no se encontro");
        }

        [HttpPut]
        [Route("product")]
        public async Task<IActionResult> UpdateProducts([FromForm] UpdateProduct model)
        {

            if (ModelState.IsValid)
            {
                var updateProduct = await _unityOfWork.ProductsRepository.GetProductAsync(model.Id);
                _unityOfWork.FilesRepository.DeleteFiles(updateProduct.Files.Id);

                if (updateProduct != null)
                {
                    updateProduct.Files = await _unityOfWork.FilesRepository.AddProductsPicture(model.Files);
                    updateProduct.Name = model.Name;
                    updateProduct.Description = model.Description;
                    updateProduct.Price = model.Price;
                    updateProduct.Category = _unityOfWork.CategoryRepository.GetFirstOrDefault(x => x.CategoryId == model.Category); ;

                    _unityOfWork.ProductsRepository.Update(updateProduct);
                    _unityOfWork.Save();
                    return Ok(model);

                }
                return BadRequest("Por favor revisa los detalles del producto");

            }
            return BadRequest("Error al actualizar el producto");
        }

        [HttpDelete]
        [Route("product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unityOfWork.ProductsRepository.GetProductAsync(id);

            if (product != null)
            {
                _unityOfWork.FilesRepository.DeleteFiles(product.Files.Id);
                _unityOfWork.ProductsRepository.Remove(product);
                _unityOfWork.Save();

                return Ok($"El producto {id} ha sido eliminado");
            }
            return BadRequest($"No existe el producto con Id: {id}");
        }
    }
}
