using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.DataModels
{
    public class Files
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string Url { get; set; }

        public DateTime uploadDateTime { get; set; }
    }
}
