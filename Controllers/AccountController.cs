using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;

namespace TP07_ROTH.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost(string Username, string Password)
        {
            if (Username == null || Username == "" || Password == null || Password == "")
            {
                ViewBag.Error = "Usuario y contraseña son obligatorios";
                return View("Login");
            }

            if (BD.VerificarContraseña(Username, Password))
            {
                Usuario usuario = BD.ObtenerPorUsername(Username);
                HttpContext.Session.SetInt32("IDdelUsuario", usuario.ID);
                HttpContext.Session.SetString("Username", usuario.Username);
                HttpContext.Session.SetString("Logeado", "true");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View("Login");
            }
        }


        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroPost(Usuario usuario)
        {
            if (usuario.Username == null || usuario.Username == "" || usuario.Password == null || usuario.Password == "")
            {
                ViewBag.Error = "Usuario y contraseña son obligatorios";
                return View("Registro");
            }

            usuario.UltimoLogin = DateTime.Now;

            if (BD.Registro(usuario))
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "El usuario ya existe";
                return View("Registro");
            }
        }


        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
