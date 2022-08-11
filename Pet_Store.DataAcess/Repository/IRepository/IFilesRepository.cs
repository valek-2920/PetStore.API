using Microsoft.AspNetCore.Http;
using Pet_Store.Domains.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.DataAcess.Repository.IRepository
{
    public interface IFilesRepository
    {
        Task<Files> AddProductsPicture(IFormFile file);
        void DeleteFiles(int id);
    }
}
