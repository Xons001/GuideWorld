using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class ListaController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        // GET: Lista
        public ActionResult Lista(int id)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            int sess = (int)Session["id"];
            var info = new AllModel
            {
                RutaModel = db.Ruta.Where(r => r.guia_id == sess).ToList(),
                MuseoModel = db.Museo.Where(m => m.guia_id == sess).ToList()
            };
            if (info.RutaModel.ToList().Count == 0)
            {
                ViewData["noguiarutas"] = "You do not have routes available";
            }
            if (info.MuseoModel.ToList().Count == 0)
            {
                ViewData["noguiamuseo"] = "You do not have museums available";
            }
            return View(info);
        }

        public ActionResult EliminarRuta(int id)
        {
            int sess = (int)Session["id"];
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
                Ruta eliminarRuta = db.Ruta.SingleOrDefault(r => r.id == id && r.guia_id == sess);
                List<ReservaOrganizaRuta> eliminarRes = db.ReservaOrganizaRuta.Where(s => s.ruta_id == id).ToList();
                db.ReservaOrganizaRuta.RemoveRange(eliminarRes);
                db.Ruta.Remove(eliminarRuta);
                db.SaveChanges();
            }

            return RedirectToAction("Lista", "Lista", new { id = sess });
        }

        public ActionResult EliminarMuseo(int id)
        {
            int sess = (int)Session["id"];
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
                Museo eliminarMuseo = db.Museo.SingleOrDefault(r => r.id == id && r.guia_id == sess);
                List<ReservaOrganizaMuseo> eliminarResm = db.ReservaOrganizaMuseo.Where(s => s.museo_id == id).ToList();
                db.ReservaOrganizaMuseo.RemoveRange(eliminarResm);
                db.Museo.Remove(eliminarMuseo);
                db.SaveChanges();
            }

            return RedirectToAction("Lista", "Lista", new { id = sess });
        }
    }
}