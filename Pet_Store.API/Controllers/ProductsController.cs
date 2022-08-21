using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using PetStore.DataAccess.Repository.UnityOfWork;
using Pet_Store.DataAcess.Data;
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

        public ProductsController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        [Route("product")]
        public IActionResult CreateProduct([FromForm] NewProduct model)
        {
            if (ModelState.IsValid)
            {

                var GetCategory = _unityOfWork.CategoryRepository.GetFirstOrDefault(x => x.CategoryId == model.Category);

                Products product = new Products
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = GetCategory,
                    Files = model.Files
                };

                _unityOfWork.ProductsRepository.Add(product);
                _unityOfWork.Save();
                return Ok(product);
            }
            return BadRequest("Error al crear el producto");
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            var allProducts = _unityOfWork.ProductsRepository.GetAll(includeProperties:"Category");
            _unityOfWork.Save();

            if (allProducts != null)
            {
                return Ok(allProducts);
            }
            return BadRequest("No hay productos");
        }

        [HttpGet]
        [Route("product")]
        public IActionResult GetProduct(int id)
        {
            var product = _unityOfWork.ProductsRepository.GetFirstOrDefault(x => x.ProductId == id);
            _unityOfWork.Save();

            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("El producto solicitado no se encontro");
        }

        [HttpPut]
        [Route("product")]
        public IActionResult UpdateProducts([FromBody] Products model)
        {

            if (ModelState.IsValid)
            {
                _unityOfWork.ProductsRepository.Update(model);
                _unityOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar el producto");
        }

        [HttpDelete]
        [Route("product")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unityOfWork.ProductsRepository.GetFirstOrDefault(x => x.ProductId == id);

            if (product != null)
            {
                _unityOfWork.ProductsRepository.Remove(product);
                _unityOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}
