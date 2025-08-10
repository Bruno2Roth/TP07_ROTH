using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;

namespace TP07_ROTH.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Logeado") != "true")
                return RedirectToAction("Login", "Account");

            int usuarioID = HttpContext.Session.GetInt32("UsuarioID") ?? 0;
            ViewBag.Tareas = BD.TraerTareas(usuarioID);
            return View();
        }

        public IActionResult CargarTareas()
        {
            if (HttpContext.Session.GetString("Logeado") != "true")
                return RedirectToAction("Login", "Account");

            int usuarioID = HttpContext.Session.GetInt32("UsuarioID") ?? 0;
            ViewBag.Tareas = BD.TraerTareas(usuarioID);
            return View("Index");
        }

        public IActionResult CrearTareas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearTareaGuardar(Tarea tarea)
        {
            tarea.IDUsuario = HttpContext.Session.GetInt32("UsuarioID") ?? 0;
            tarea.Eliminada = false;
            tarea.Finalizada = false;
            tarea.Fecha = DateTime.Now;

            if (BD.CrearTarea(tarea))
                return RedirectToAction("Index");

            ViewBag.Error = true;
            return View("CrearTareas");
        }

        public IActionResult FinalizarTarea(int id)
        {
            BD.FinalizarTarea(id);
            return RedirectToAction("Index");
        }

        public IActionResult EliminarTarea(int id)
        {
            BD.EliminarTarea(id);
            return RedirectToAction("Index");
        }

        public IActionResult EditarTarea(int id)
        {
            ViewBag.Tarea = BD.TraerTarea(id);
            return View();
        }

        [HttpPost]
        public IActionResult EditarTareaGuardar(Tarea tarea)
        {
            if (BD.ActualizarTarea(tarea))
                return RedirectToAction("Index");

            ViewBag.Error = true;
            ViewBag.Tarea = tarea;
            return View("EditarTarea");
        }
    }
}
