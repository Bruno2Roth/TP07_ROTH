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
            return View();
        }


        public IActionResult CargarTareas()
        {
            if (HttpContext.Session.GetString("Logeado") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");

            Usuario usuario = BD.ObtenerPorUsername(username);
            int IDdelUsuario = usuario.ID;

            ViewBag.Tareas = BD.TraerTareas(IDdelUsuario);

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
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            tarea.IDUsuario = usuario.ID;
            tarea.Eliminada = false;
            tarea.Finalizada = false;
            tarea.Fecha = DateTime.Now;

            if (BD.CrearTarea(tarea))
                return RedirectToAction("Index");

            ViewBag.Error = true;
            return View("CrearTarea");
        }

        public IActionResult FinalizarTarea(int IDdelaTarea)
        {
            BD.FinalizarTarea(IDdelaTarea);
            return RedirectToAction("Index");
        }

        public IActionResult EliminarTarea(int IDdelaTarea)
        {
            BD.EliminarTarea(IDdelaTarea);
            return RedirectToAction("Index");
        }

        public IActionResult ModificarTarea(int id)
        {
            ViewBag.Tarea = BD.TraerTarea(id);
            return View();
        }

        [HttpPost]
        public IActionResult ModificarTareaGuardar(Tarea tarea)
        {
            if (BD.ActualizarTarea(tarea))
                return RedirectToAction("VerTareas");

            ViewBag.Error = true;
            ViewBag.Tarea = tarea;
            return View("ModificarTarea");
        }
    }
}
