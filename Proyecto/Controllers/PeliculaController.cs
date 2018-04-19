using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Newtonsoft.Json;

namespace Proyecto.Controllers
{
    public class PeliculaController : Controller
    {
        // GET: Pelicula
        Data wrap = new Data();
        List<Pelicula> na = new List<Pelicula>();
        ArbolB<string> showName = new ArbolB<string>();
        ArbolB<string> showYear = new ArbolB<string>();
        ArbolB<string> showGender = new ArbolB<string>();
        ArbolB<string> movieName = new ArbolB<string>();
        ArbolB<string> movieYear = new ArbolB<string>();
        ArbolB<string> movieGender = new ArbolB<string>();
        ArbolB<string> documentaryName = new ArbolB<string>();
        ArbolB<string> documentaryYear = new ArbolB<string>();
        ArbolB<string> documentaryGender = new ArbolB<string>();

        public ActionResult Index()
        {
            foreach (var item in wrap.a1)
            {
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

                wrap.a1 = na;
                
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
