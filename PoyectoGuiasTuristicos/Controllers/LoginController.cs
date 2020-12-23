using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace PoyectoGuiasTuristicos.Controllers
{
    public class LoginController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        // GET: Login
        public ActionResult Login()
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Usuario user)
        {
            if (ModelState.IsValid)
            {
                var passd = db.Usuario.FirstOrDefault(s => s.correo == user.correo);
                if (passd != null)
                {
                    string passDecrypt = DecryptBase64(passd.contrasenya.ToString());
                    var check = (from u in db.Usuario where u.correo == user.correo && passDecrypt == user.contrasenya select u).SingleOrDefault();
                    if (check != null)
                    {
                        Session["id"] = check.id;
                        Session["nombre"] = check.nombre_usuario;
                        Session["tipousuario"] = check.tipoUsuario;
                        return RedirectToAction("../Home/Index");
                    }
                    else
                    {
                        ViewBag.Error = "Wrong password";
                    }
                }
                else
                {
                    ViewBag.Error = "Any user with this mail";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("../Home/Index");
        }
        [HttpGet]
        public ActionResult EditarPerfil()
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            Usuario user = null;
            if (ModelState.IsValid)
            {
                int id = (int)Session["id"];
                user = db.Usuario.FirstOrDefault(s => s.id.Equals(id));
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    ViewBag.Error = "";
                }
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil(Usuario user)
        {
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Selecciona country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }
            if (ModelState.IsValid)
            {
                var check = db.Usuario.FirstOrDefault(s => s.id == user.id);
                if (check != null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    check.nombre = user.nombre;
                    check.apellidos = user.apellidos;
                    check.nombre_usuario = user.nombre_usuario;
                    check.descripcion = user.descripcion;
                    check.telf = user.telf;
                    db.SaveChanges();
                    ViewData["modificado"] = "Profile successfully updated";

                    return View();
                }
                else
                {
                    ViewBag.Error = "The profile can't be updated";
                }
            }

            return View();
        }

        public ActionResult Buscar(DateTime? fecha, int? ciudades, int? paises)
        {
            var buscar = new AllModel();
            var country = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select country--", Value = "0" });

            foreach (var m in country)
            {
                li.Add(new SelectListItem { Text = m.nombre, Value = m.id.ToString() });
                ViewBag.paises = li;
            }

            if (ciudades == null)
            {
                fecha = fecha.Value.Add(new TimeSpan(0, 0, 0));
                var fecha2 = fecha.Value.Add(new TimeSpan(23, 59, 59));
                buscar = new AllModel()
                {
                    RutaModel = db.Ruta.Where(s => s.fechaInicio >= fecha && s.fechaInicio <= fecha2).ToList(),
                    MuseoModel = db.Museo.Where(s => s.fechaInicio >= fecha && s.fechaInicio <= fecha2).ToList(),
                };
            }
            else if (fecha == null)
            {
                buscar = new AllModel()
                {
                    RutaModel = db.Ruta.Where(s => s.ciudad_id == ciudades).ToList(),
                    MuseoModel = db.Museo.Where(s => s.ciudad_id == ciudades).ToList(),
                };
            }
            else
            {
                fecha = fecha.Value.Add(new TimeSpan(0, 0, 0));
                var fecha2 = fecha.Value.Add(new TimeSpan(23, 59, 59));
                buscar = new AllModel()
                {
                    RutaModel = db.Ruta.Where(s => s.fechaInicio >= fecha && s.fechaInicio <= fecha2 && s.ciudad_id == ciudades).ToList(),
                    MuseoModel = db.Museo.Where(s => s.fechaInicio >= fecha && s.fechaInicio <= fecha2 && s.ciudad_id == ciudades).ToList(),
                };
            }
            if (buscar.MuseoModel.ToList().Count == 0) ViewData["nomuseos"] = "There are no museums subject to these conditions";
            if (buscar.RutaModel.ToList().Count == 0) ViewData["norutas"] = "There are no rutes subject to these conditions";
            return View(buscar);
        }
        public static string DecryptBase64(string value)
        {
            string result = "";

            try
            {
                byte[] bytes = Convert.FromBase64String(value);

                result = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch { }

            return result;
        }
    }
}