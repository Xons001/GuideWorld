using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class EditarController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        // GET: EditarRuta
        [HttpGet]
        public ActionResult EditarRuta(int id)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            var ruta = db.Ruta.FirstOrDefault(r => r.id == id);
            return View(ruta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarRuta(Ruta ruta)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            if (ModelState.IsValid)
            {
                var check = db.Ruta.FirstOrDefault(s => s.id == ruta.id);
                if (check != null)
                {

                    db.Configuration.ValidateOnSaveEnabled = false;
                    check.fechaInicio = ruta.fechaInicio;
                    check.fechaFinal = ruta.fechaFinal;
                    check.plazas = ruta.plazas;
                    check.precio = ruta.precio;
                    check.descripcion = ruta.descripcion;
                    check.fotografia = ruta.fotografia;
                    db.SaveChanges();
                    ViewData["modificada"] = "Route successfully updated";
                }
                return View(check);
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditarMuseo(int id)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            var museo = db.Museo.FirstOrDefault(r => r.id == id);
            return View(museo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMuseo(Museo museo)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            if (ModelState.IsValid)
            {
                var check = db.Museo.FirstOrDefault(s => s.id == museo.id);
                if (check != null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    check.fechaInicio = museo.fechaInicio;
                    check.fechaFin = museo.fechaFin;
                    check.plazas = museo.plazas;
                    check.precio = museo.precio;
                    check.fotografia = museo.fotografia;
                    db.SaveChanges();
                    ViewData["modificado"] = "Museum successfully updated";
                }
                return View(check);
            }

            return View();
        }
    }
}