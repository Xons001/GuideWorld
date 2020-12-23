using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class GuiasController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();

        [HttpGet]
        public ActionResult Guias(int id)
        {
            Session["guia"] = id;
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            var rutaGuia = new AllModel
            {
                UsuarioModel = db.Usuario.Where(s => s.id == id).ToList(),
                RutaModel = db.Ruta.Where(r => r.guia_id == id).ToList(),
                MuseoModel = db.Museo.Where(s => s.guia_id == id).ToList(),
                ValoracionModel = db.Valoracion.Where(s => s.usuario_guia_id == id).ToList()
            };
            if (rutaGuia.RutaModel.ToList().Count == 0)
            {
                ViewData["norutas"] = "This guide does not have routes available yet";
            }
            if (rutaGuia.MuseoModel.ToList().Count == 0)
            {
                ViewData["nomuseo"] = "This guide does not have museums available yet";
            }
            if (rutaGuia.ValoracionModel.ToList().Count == 0)
            {
                ViewData["novaloracion"] = "This guide has not been rated yet";
            }
            var avg = db.Valoracion.Where(s => s.usuario_guia_id == id).ToList();
            if (avg == null) ViewBag.Error = "This guide has not been rated yet";
            else
            {
                int res = 0;
                for (var i = 0; i < avg.Count; i++)
                {
                    res += (int)avg[i].nota;
                }
                ViewData["valoracio"] = res.ToString();
            }

            return View(rutaGuia);
        }

        [HttpPost]
        public ActionResult Guias(AllModel all)
        {

            int sess = (int)Session["id"];
            int id_guia = (int)Session["guia"];
            Valoracion valoracion = all.valoracionModels;

            if (ModelState.IsValid)
            {
                valoracion.usuario_turista_id = sess;
                valoracion.usuario_guia_id = id_guia;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Valoracion.Add(valoracion);
                db.SaveChanges();
            }
            return RedirectToAction("Guias", "Guias", new { id = id_guia });
        }
    }
}