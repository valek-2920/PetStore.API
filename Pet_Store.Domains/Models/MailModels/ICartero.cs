using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.MailModels
{
    public interface ICartero
    {
        void Enviar(CorreoElectronico correo);
    }
}
