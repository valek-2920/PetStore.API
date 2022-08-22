using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.MailModels
{
    public class CorreoElectronico
    {
        public string Destinatario { get; set; }

        public string Asunto { get; set; }

        public string Cuerpo { get; set; }
    }
}
