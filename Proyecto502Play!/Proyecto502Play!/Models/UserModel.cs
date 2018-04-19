using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto502Play_.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name ="Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Edad")]
        public int edad { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string password { get; set; }
    }
}