using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;

namespace TP07_ROTH.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargarTareas()
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                ViewBag.Error = "Sesion no encontrada";
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            if (usuario == null)
            {
                ViewBag.Error = "Usuario no encontrado";
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Tareas = BD.TraerTareas(usuario.ID);
            return View("VerTarea");
        }

        public IActionResult VerTareas()
        {
            return CargarTareas();
        }
        public IActionResult CrearTarea()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearTareaGuardar(Tarea tarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                ViewBag.Error("Sesion no encontrada");
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            tarea.IDUsuario = usuario.ID;
            tarea.Eliminada = false;
            tarea.Finalizada = false;

            if (BD.CrearTarea(tarea))
                return RedirectToAction("Index");
            return View("CrearTarea");
        }
        public IActionResult FinalizarTarea(int IDdelaTarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                ViewBag.Error("Sesion no encontrada");
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            BD.FinalizarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }
        public IActionResult EliminarTarea(int IDdelaTarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                ViewBag.Error("Sesion no encontrada");
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            BD.EliminarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }
        public IActionResult ModificarTarea(int ID)
        {
            return View(BD.TraerTarea(ID));
        }

        [HttpPost]
        public IActionResult ModificarTareaGuardar(Tarea tarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            if (BD.ActualizarTarea(tarea))
            {
                Console.WriteLine("we");
                return RedirectToAction("Index");
            }
            Console.WriteLine("Rroror");
            return View("VerTarea");
        }
    }
}
