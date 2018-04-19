using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto502Play_.Models;

namespace Biblio
{
    public class ManejadorLogin
    {
        public IList<UserModel> Usuarios { get; set; }

        public IList<UserModel> ObtenerUsuarios()
        {
            return Usuarios;
        }
    }
}
