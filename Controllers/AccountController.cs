using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;
using System;

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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return View("Login");
            }

            if (BD.VerificarContraseña(Username, Password))
            {
                Usuario usuario = BD.ObtenerPorUsername(Username);
                HttpContext.Session.SetString("IDdelUsuario", usuario.ID.ToString());
                HttpContext.Session.SetString("Username", usuario.Username);
                HttpContext.Session.SetString("EstaLogin", "true");
                return RedirectToAction("Index", "Home");
            }

            return View("Login");
        }



        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroPost(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Username) || string.IsNullOrEmpty(usuario.Password))
            {
                return View("Registro");
            }

            usuario.UltimoLogin = DateTime.Now;

            if (BD.Registro(usuario))
            {
                return View("Login");
            }
            else
            {
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
