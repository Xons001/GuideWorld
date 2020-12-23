using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class MuseoController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        [HttpGet]
        public ActionResult Museo(int id)
        {
            var rutaGuia = new AllModel()
            {
                MuseoModel = db.Museo.Where(s => s.id == id).ToList(),
                ReservaMuseoModel = db.ReservaOrganizaMuseo.Where(r => r.museo_id == id).ToList()
            };
            if (rutaGuia.ReservaMuseoModel.Count() == 0)
            {
                ViewData["noreserve"] = "There aren't reserves";
            }
            if (Session["id"] != null)
            {
                int id_user = int.Parse(Session["id"].ToString());
                var check = db.ReservaOrganizaMuseo.SingleOrDefault(s => s.usuario_id == id_user && s.museo_id == id);
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
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            if (rutaGuia.ReservaRutaModel != null)
            {
                if (rutaGuia.ReservaRutaModel.Count() == rutaGuia.RutaModel.FirstOrDefault(r => r.id == id).plazas)
                {
                    ViewBag.disable = "No";
                }
            }

            return View(rutaGuia);
        }

        [HttpPost]
        public ActionResult Museo(Museo museo)
        {
            if (ModelState.IsValid)
            {
                if (Session["id"] != null)
                {
                    int sess = int.Parse(Session["id"].ToString());
                    var check = db.ReservaOrganizaMuseo.SingleOrDefault(s => s.usuario_id == sess && s.museo_id == museo.id);
                    if (check == null)
                    {
                        ReservaOrganizaMuseo rr = new ReservaOrganizaMuseo();
                        rr.museo_id = museo.id;
                        rr.usuario_id = sess;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.ReservaOrganizaMuseo.Add(rr);
                        db.SaveChanges();
                        Session["reservador"] = "si";

                    }
                    else
                    {
                        ReservaOrganizaMuseo eliminar = db.ReservaOrganizaMuseo.SingleOrDefault(s => s.usuario_id == sess && s.museo_id == museo.id);
                        db.ReservaOrganizaMuseo.Remove(eliminar);
                        db.SaveChanges();
                        Session["reservador"] = "no";
                    }
                }
                else ViewData["nologin"] = "Login";

            }
            return RedirectToAction("Ruta", "Ruta", new { id = museo.id });
        }
    }
}