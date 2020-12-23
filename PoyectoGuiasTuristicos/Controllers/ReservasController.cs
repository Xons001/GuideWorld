using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class ReservasController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        // GET: Reservas
        public ActionResult Reservas()
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            int sess = int.Parse(Session["id"].ToString());
            var ruta = (from r in db.Ruta
                        join rr in db.ReservaOrganizaRuta on r.id equals rr.ruta_id into table1
                        from rr in table1.ToList()
                        where rr.ruta_id == r.id && rr.usuario_id == sess
                        select r).ToList();
            var museo = (from m in db.Museo
                        join rm in db.ReservaOrganizaMuseo on m.id equals rm.museo_id into table1
                        from rm in table1.ToList()
                        where rm.museo_id == m.id && rm.usuario_id == sess
                        select m).ToList();
            
            var reservas = new AllModel()
            {
                RutaModel = ruta,
                MuseoModel = museo
            };
            return View(reservas);
        }
    }
}