using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Pet_Store.DataAcess.Repository.IRepository;
using Pet_Store.Domains.Models.DataModels;
using PetStore.DataAccess.Repository;
using Project_PetStore.API.DataAccess;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Pet_Store.DataAcess.Repository
{
    public class FilesRepository : Repository<Files>, IFilesRepository
    {
        readonly ApplicationDbContext _context;
        readonly IHostEnvironment _hostEnvironment;

        public FilesRepository(ApplicationDbContext context, IHostEnvironment hostEnvironment) : base(context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public async Task<Files> AddProductsPicture(IFormFile file)
        {
            Files obj = new Files();
            string wwwPath = _hostEnvironment.ContentRootPath;

            if (file != null)
            {
                var uploads = Path.Combine(wwwPath, @"Resources\ProductsPicture");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }


                using (var FileStreams = new FileStream(Path.Combine(uploads, file.FileName),
                    FileMode.Create))
                {
                    file.CopyTo(FileStreams);
                    obj = new Files()
                    {
                        Name = file.FileName,
                        Size = file.Length,
                        Url = @"\Resources\ProductsPicture\" + file.FileName,
                        uploadDateTime = DateTime.Now
                    };
                }
                if (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/jpg")
                {

                    return obj;
                }
            }
            return null;
        }

        public async void DeleteFiles(int id)
        {
            try
            {
                var result = await (from f in _context.Files
                                    where f.Id == id
                                    select f).FirstOrDefaultAsync();

                if (result != null)
                {

                    string wwwPath = _hostEnvironment.ContentRootPath;
                    var uploads = Path.Combine(wwwPath, @"Resources\ProductsPicture");
                    var oldImagePath = Path.Combine(uploads, result.Name.TrimStart('\\'));

                    System.IO.File.Delete(oldImagePath);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
