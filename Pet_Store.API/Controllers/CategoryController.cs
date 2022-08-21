using Microsoft.AspNetCore.Mvc;
using Pet_Store.DataAcess.Repository.UnityOfWork;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public CategoryController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        [Route("category")]
        public IActionResult CreateCategory([FromBody] NewCategory model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Description = model.Description
                };

                _unityOfWork.CategoryRepository.Add(category);
                _unityOfWork.Save();
                return Ok(category);
            }
            return BadRequest("Error al crear categoria");
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult GetCategories()
        {
            var allCategories = _unityOfWork.CategoryRepository.GetAll();
            _unityOfWork.Save();

            if (allCategories != null)
            {
                return Ok(allCategories);
            }
            return BadRequest("No hay categorias");
        }

        [HttpGet]
        [Route("category")]
        public IActionResult GetCategory(int id)
        {
            var category = _unityOfWork.CategoryRepository.GetFirstOrDefault(x => x.CategoryId == id);
            _unityOfWork.Save();

            if (category != null)
            {
                return Ok(category);
            }
            return BadRequest("La categoria solicitada no se encontro");
        }

        [HttpPut]
        [Route("category")]
        public IActionResult UpdateCategories([FromBody] Category model)
        {

            if (ModelState.IsValid)
            {
                _unityOfWork.CategoryRepository.Update(model);
                _unityOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar categoria");
        }

        [HttpDelete]
        [Route("category")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unityOfWork.CategoryRepository.GetFirstOrDefault(x => x.CategoryId == id);

            if (category != null)
            {
                _unityOfWork.CategoryRepository.Remove(category);
                _unityOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}