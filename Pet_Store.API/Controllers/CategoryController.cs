using Microsoft.AspNetCore.Mvc;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Category> _categoryRepository;

        public CategoryController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.Repository<Category>();
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

                _categoryRepository.Add(category);
                _unitOfWork.Save();
                return Ok(category);
            }
            return BadRequest("Error al crear categoria");
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult GetCategories()
        {
            var allCategories = _categoryRepository.GetAll();
            _unitOfWork.Save();

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
            var category = _categoryRepository.GetFirstOrDefault(x => x.CategoryId == id);
            _unitOfWork.Save();

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
                _categoryRepository.Update(model);
                _unitOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar categoria");
        }

        [HttpDelete]
        [Route("category")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.GetFirstOrDefault(x => x.CategoryId == id);

            if (category != null)
            {
                _categoryRepository.Remove(category);
                _unitOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}