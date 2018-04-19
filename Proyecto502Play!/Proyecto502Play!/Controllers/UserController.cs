using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Proyecto502Play_.Models;

namespace Proyecto502Play_.Controllers
{
    public class UserController : Controller
    {
        List<UserModel> ListaUsuario = new List<UserModel>();
        private UserModel Usuario;
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            Usuario = new UserModel();
            Usuario.username = "Admin";
            Usuario.password = "Admin";
            Usuario.nombre = "Administrador";
            Usuario.apellido = ":)";
            Usuario.edad = 20;

            ListaUsuario.Add(Usuario);

            return View(ListaUsuario);
        }

        public ActionResult CargaUsuarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargaUsuarios(HttpPostedFileBase postedFile)
        {
            var FilePath = string.Empty;
            //AQUI VOY VIENDO LO DE LA CARGA DE ARCHIVOS!
            
                if (postedFile != null)
                {
                    FilePath = Path.GetFullPath(postedFile.FileName);

                    var contenido = System.IO.File.ReadAllText(FilePath);
                    ListaUsuario = JsonConvert.DeserializeObject<List<UserModel>>(contenido);
                 }


            return RedirectToAction("Admin", "User");
        }

        public ActionResult logIn()
        {
            return View();
        }

        //Verificar los datos suministrados por el usuario al realizar la petición del Post de envío de información mediante la 
        //GUI de autenticación de la aplicación
        [HttpPost]
        public ActionResult logIn(Models.UserModel user)
        {
            if (user.username == "admin" && user.password == "admin")
            {
                
                return RedirectToAction("Index", "Home"); //MODICAR ESTO
            }

            if (ModelState.IsValid) //Se verifica que el modelo de datos sea válido. (Propiedades)
            {
                if (IsValid(user.username, user.password)) //Verificar que el email y clave exista utilizando el método privado
                {
                    FormsAuthentication.SetAuthCookie(user.username,false); //crea variable de usuario con el correo del usuario
                    return RedirectToAction("Index","Home"); //dirigir al controlador home vista Index una vez se a autenticado en el sistema
                }
                else
                {
                    ModelState.AddModelError("Datos incorrectos", "Login incorrecto");
                }
            }
            return View(user);
        }

        //Realiza el llamado de la vista, para registrarse en el sistema
        public ActionResult Registration()
        {
            return View();
        }

        //Verifica los datos por el usuario al realizar la petición del POST de envío de
        //info, para crear un nuevo usuario en el sistema
        [HttpPost]
        public ActionResult Registration(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                
                Usuario = new UserModel();
                Usuario.nombre = user.nombre;
                Usuario.apellido = user.apellido;
                Usuario.edad = user.edad;
                Usuario.username = user.username;
                Usuario.password = user.password;

                ListaUsuario.Add(Usuario);
            }
            else
            {
                ModelState.AddModelError("Datos incorrectos", "Registro incorrecto");
            }
            return View();
        }

        //Cerrar Sesión del usuario que ya está autenticado 
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        //Método para vlidar el usarname y el password, realiza la consulta en la lista de usuarios
        private bool IsValid(string username, string Password)
        {
            bool IsValid = false;

            var user = ListaUsuario.FirstOrDefault(u => u.username == username);

            if (user != null)
            {
                if (user.password == Password)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }
    }
}