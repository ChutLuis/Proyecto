using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Newtonsoft.Json;
using System.Security.Permissions;

namespace Proyecto.Controllers
{
    public class PeliculaController : Controller
    {
        // GET: Pelicula
        
        List<Pelicula> na = new List<Pelicula>();
        ArbolB<string> showName = new ArbolB<string>();
        ArbolB<int> showYear = new ArbolB<int>();
        ArbolB<string> showGender = new ArbolB<string>();
        ArbolB<string> movieName = new ArbolB<string>();
        ArbolB<int> movieYear = new ArbolB<int>();
        ArbolB<string> movieGender = new ArbolB<string>();
        ArbolB<string> documentaryName = new ArbolB<string>();
        ArbolB<int> documentaryYear = new ArbolB<int>();
        ArbolB<string> documentaryGender = new ArbolB<string>();

        [UIPermission(SecurityAction.Demand, Unrestricted = true)]
        public ActionResult Index()
        {
            foreach (Pelicula peli in Data.Instance.a1)
            {
                if (peli.type== "Show")
                {
                    showName.Insertar(peli.name);
                    showGender.Insertar(peli.genre);
                    showYear.Insertar(peli.year);
                }
                else if (peli.type=="Película")
                {
                    movieName.Insertar(peli.name);
                    movieYear.Insertar(peli.year);
                    movieGender.Insertar(peli.genre);
                }
                else if (peli.type=="Documental")
                {
                    documentaryName.Insertar(peli.name);
                    documentaryGender.Insertar(peli.genre);
                    documentaryYear.Insertar(peli.year);
                }

                string _path = @"C:\Users\Luis\Documents\Visual Studio 2015\Projects\Project\Proyecto\Proyecto\UploadedFiles";
                using (var fs = new FileStream(_path, FileMode.OpenOrCreate))
                {
                    int x = 0;
                    foreach (var keys in showName.Inorder())
                    {                        
                        fs.Write(ByteGenerator.ConverToBytes(Data.Instance.a1[x].ToFixedSizeString()), 0, 127);
                        x++;
                    }
                }
                var buffer = new byte[Pelicula.FixedSize];
                using (var fs = new FileStream(_path, FileMode.OpenOrCreate))
                {
                    fs.Seek(Pelicula.FixedSize, SeekOrigin.Begin);
                    fs.Read(buffer, 0, Pelicula.FixedSize);
                }
                var nodeString = ByteGenerator.ConvertToString(buffer);
                var values = nodeString.Split('|');
                var pelicula = new Pelicula
                {
                    type = values[0].Trim(),
                    name = values[1].Trim(),
                    year = int.Parse(values[2].Trim()),
                    genre = values[3].Trim(),
                };


            }
            
                
            return View();
        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string _path = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";

                var contenido = System.IO.File.ReadAllText(_path);
                na = JsonConvert.DeserializeObject<List<Pelicula>>(contenido);

               Data.Instance.a1 = na;
                
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                
            }

            return View("Index");
        }
        // GET: Pelicula/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pelicula/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pelicula/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pelicula/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pelicula/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pelicula/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pelicula/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
