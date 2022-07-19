using Microsoft.AspNetCore.Mvc;
using PetStore.DataAccess.Repository.UnityOfWork;
using PetStore.Domain.Models.ViewModels;
using Project_PetStore.API.Models.DataModels;

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
        public IActionResult CreateProduct([FromBody] NewProduct model)
        {
            if (ModelState.IsValid)
            {
                Products product = new Products
                {
                    Name = model.Name,
                    ListPrice = model.ListPrice,
                    Price = model.Price,
                    CategoryId = model.Category
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
            var allProducts = _unityOfWork.ProductsRepository.GetAll();
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

                return Ok($"El producto {id} ha sido eliminado");
            }
            return BadRequest($"No existe el producto con Id: {id}");
        }
    }
}
