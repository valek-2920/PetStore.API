﻿using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.DataAcess.Repository;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Category> _categoryRepository;
        readonly IRepository<Products> _productsRepository;


        public ProductsController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.Repository<Category>();
            _productsRepository = _unitOfWork.Repository<Products>();
        }

        [HttpPost]
        [Route("product")]
        public IActionResult CreateProduct([FromForm] NewProduct model)
        {
            if (ModelState.IsValid)
            {

                var GetCategory = _categoryRepository.GetFirstOrDefault(x => x.CategoryId == model.Category);

                Products product = new Products
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = GetCategory,
                    Files = model.Files
                };

                _productsRepository.Add(product);
                _unitOfWork.Save();
                return Ok(product);
            }
            return BadRequest("Error al crear el producto");
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            var allProducts = _productsRepository.GetAll(includeProperties:"Category");
            _unitOfWork.Save();

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
            var product = _productsRepository.GetFirstOrDefault(x => x.ProductId == id);
            _unitOfWork.Save();

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
                _productsRepository.Update(model);
                _unitOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar el producto");
        }

        [HttpDelete]
        [Route("product")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productsRepository.GetFirstOrDefault(x => x.ProductId == id);

            if (product != null)
            {
                _productsRepository.Remove(product);
                _unitOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}
