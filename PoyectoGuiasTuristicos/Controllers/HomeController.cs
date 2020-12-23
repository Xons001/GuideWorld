using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class HomeController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var newR = from r in db.Ruta
                       select r;
            var newM = from r in db.Museo
                       select r;
            var ruta = new AllModel
            {
                UsuarioModel = db.Usuario.Where(s => s.tipoUsuario == true).ToList(),
                RutaModel = newR.ToList(),
                MuseoModel = newM.ToList()
            };

            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            return View(ruta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscar()
        {
            return View();
        }

        public JsonResult getCiudad(int id)
        {
            var states = db.Ciudad.Where(c => c.pais_id == id).ToList();
            List<SelectListItem> listaciudades = new List<SelectListItem>();

            listaciudades.Add(new SelectListItem { Text = "--Select city--", Value = "0" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listaciudades.Add(new SelectListItem { Text = x.nombre, Value = x.id.ToString() });
                }
            }
            return Json(new SelectList(listaciudades, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
    }
}