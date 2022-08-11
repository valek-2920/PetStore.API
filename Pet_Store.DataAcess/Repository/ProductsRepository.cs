using Microsoft.EntityFrameworkCore;
using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using PetStore.Domain.Models.ViewModels;
using Project_PetStore.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.DataAccess.Repository
{
    public class ProductsRepository : Repository<Products>, IProductRepository
    {
        readonly ApplicationDbContext _context;
        readonly IFilesRepository _filesRepository;
        readonly ICategoryRepository _categoryRepository;


        public ProductsRepository(
            ApplicationDbContext context,
            IFilesRepository filesRepository,
            ICategoryRepository categoryRepository) : base(context)
        {
            _context = context;
            _filesRepository = filesRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Products> CreateProduct(NewProduct model)
        {
            try
            {
                var productExist = ProductExist(model.Name);

                if (!productExist)
                {
                    var upload = await _filesRepository.AddProductsPicture(model.Files);
                    var category = _categoryRepository.GetFirstOrDefault(x => x.CategoryId == model.Category);

                    Products newProduct = new Products()
                    {
                        Files = upload,
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Category = category
                    };

                    await _context.Products.AddAsync(newProduct);
                    await _context.SaveChangesAsync();

                    return newProduct;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void Update(Products model)
        {
            _context.Products.Update(model);
        }

        public async Task<List<Products>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(x => x.Files)
                    .Include(c => c.Category)
                    .ToListAsync();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Products> GetProductAsync(int id)
        {
            try
            {
                var product = await (from x in _context.Products.
                                       Include(x => x.Files).
                                       Include(c => c.Category)
                                       where x.ProductId == id
                                       select x).FirstOrDefaultAsync();

                if(product != null)
                {
                    return product;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ProductExist(string name)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Name == name);

                if (product != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}
