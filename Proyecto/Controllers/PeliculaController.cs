using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class PeliculaController : Controller
    {
        // GET: Pelicula
        public ActionResult Index()
        {

            return View();
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
