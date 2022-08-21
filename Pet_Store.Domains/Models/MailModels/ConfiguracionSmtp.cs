using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.MailModels
{
    public class ConfiguracionSmtp
    {
        public string Remitente { get; set; }

        public string Usuario { get; set; }

        public string Contrasena { get; set; }

        public string Servidor { get; set; }

        public int Puerto { get; set; }

    }
}
