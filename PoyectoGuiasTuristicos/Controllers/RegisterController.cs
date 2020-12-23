using PoyectoGuiasTuristicos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoyectoGuiasTuristicos.Controllers
{            
    public class RegisterController : Controller
    {
        private dbGuiasTuristicosEntities db = new dbGuiasTuristicosEntities();
        //Ventana Rigistrar
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Usuario.FirstOrDefault(s => s.correo == user.correo);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    user.contrasenya = EncryptBase64(user.contrasenya);
                    db.Usuario.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.Error = "There is already an account with this email";
                }

            }
            return View();
        }
        public static string EncryptBase64(string value)
        {
            string result = "";
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] bytes = System.Text.UnicodeEncoding.UTF8.GetBytes(value);
                    result = Convert.ToBase64String(bytes);
                }
            }
            catch { }
            return result;
        }
    }
}
