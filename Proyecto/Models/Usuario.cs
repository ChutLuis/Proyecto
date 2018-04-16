using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Usuario
    {
        public string User { get; set; }
       
        public string Contraseña { get; set; }

        public bool isAdmin {get; set;}


    }
}