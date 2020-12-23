using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class RutaController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();

        [HttpGet]
        public ActionResult Ruta(int id)
        {
            var rutaGuia = new AllModel()
            {
                RutaModel = db.Ruta.Where(s => s.id == id).ToList(),
                ReservaRutaModel = db.ReservaOrganizaRuta.Where(r => r.ruta_id == id).ToList()
            };
            if(rutaGuia.ReservaRutaModel.Count() == 0)
            {
                ViewData["noreserveRuta"] = "There aren't reserves";
            }
            if (Session["id"] != null)
            {
                int id_user = int.Parse(Session["id"].ToString());
                var check = db.ReservaOrganizaRuta.SingleOrDefault(s => s.usuario_id == id_user && s.ruta_id == id);
                if (check != null)
                {
                    Session["reservador"] = "si";
                }
                else
                {
                    Session["reservador"] = "no";
                }
            }
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Selecciona Pais--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            return View(rutaGuia);
        }

        [HttpPost]
        public ActionResult Ruta(Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                if (Session["id"] != null)
                {
                    int sess = int.Parse(Session["id"].ToString());
                    var check = db.ReservaOrganizaRuta.SingleOrDefault(s => s.usuario_id == sess && s.ruta_id == ruta.id);
                    if (check == null)
                    {
                        ReservaOrganizaRuta rr = new ReservaOrganizaRuta();
                        rr.ruta_id = ruta.id;
                        rr.usuario_id = sess;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.ReservaOrganizaRuta.Add(rr);
                        db.SaveChanges();
                        Session["reservador"] = "si";

                    }
                    else
                    {
                        ReservaOrganizaRuta eliminar = db.ReservaOrganizaRuta.SingleOrDefault(s => s.usuario_id == sess && s.ruta_id == ruta.id);
                        db.ReservaOrganizaRuta.Remove(eliminar);
                        db.SaveChanges();
                        Session["reservador"] = "no";
                    }
                }
                else ViewData["nologin"] = "Login";

            }
            return RedirectToAction("Ruta","Ruta",new { id = ruta.id });
        }
    }
}