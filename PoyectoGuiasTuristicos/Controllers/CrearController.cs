using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class CrearController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        // GET: Crear
        public ActionResult CrearRuta()
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            List<SelectListItem> ciud = new List<SelectListItem>();

            ciud.Add(new SelectListItem { Text = "--Select city--", Value = "0" });
            var ciudades = db.Ciudad.ToList();
            foreach (var m in ciudades)
            {
                ciud.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.ciudades = ciud;
            }
            return View();
        }
        public ActionResult CrearMuseo()
        {
            List<SelectListItem> ciudad = new List<SelectListItem>();

            ciudad.Add(new SelectListItem { Text = "--Select city--", Value = "0" });
            var ciudade = db.Ciudad.ToList();
            foreach (var m in ciudade)
            {
                ciudad.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.ciudades = ciudad;
            }

            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select city--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CrearRuta(Ruta ruta, int ciudades)
        {
            List<SelectListItem> ciudad = new List<SelectListItem>();

            ciudad.Add(new SelectListItem { Text = "--Select city--", Value = "0" });
            var ciudade = db.Ciudad.ToList();
            foreach (var m in ciudade)
            {
                ciudad.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.ciudades = ciudad;
            }
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select city--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            ruta.guia_id = (int)Session["id"];
            ruta.ciudad_id = ciudades;
            ruta.fotografia = "../../Content/img/" + ruta.fotografia;
            db.Ruta.Add(ruta);
            db.SaveChanges();
            ViewData["nuevaruta"] = "Route created";
            ViewData["imagen"] = ruta.fotografia;
            return View();
        }
        [HttpPost]
        public ActionResult CrearMuseo(Museo museo, int ciudades)
        {
            List<SelectListItem> ciudad = new List<SelectListItem>();

            ciudad.Add(new SelectListItem { Text = "--Select city--", Value = "0" });
            var ciudade = db.Ciudad.ToList();
            foreach (var m in ciudade)
            {
                ciudad.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.ciudades = ciudad;
            }
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            db.Configuration.ValidateOnSaveEnabled = false;
            museo.guia_id = (int)Session["id"];
            museo.ciudad_id = ciudades;
            museo.fotografia = "https://caminosdepasion.com/wp-content/uploads/museo-adolfo-lozano-sidro-2-PRIEGO-DE-CORDOBA-CAMINOS-DE-PASION.jpg";
            db.Museo.Add(museo);
            db.SaveChanges();
            ViewData["nuevomuseo"] = "Museum created";
            return View();
        }
}
}