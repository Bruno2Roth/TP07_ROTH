using Microsoft.AspNetCore.Mvc;
using TP07_ROTH.Models;

namespace TP07_ROTH.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult CargarTareas()
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
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
            return View("CrearTarea");
        }
        public IActionResult FinalizarTarea(int IDdelaTarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            BD.FinalizarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }
        public IActionResult EliminarTarea(int IDdelaTarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            BD.EliminarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }
            public IActionResult ModificarTarea(int id)
            {
                if (HttpContext.Session.GetString("EstaLogin") != "true")
                    return RedirectToAction("Login", "Account");

                Tarea tarea = BD.TraerTarea(id);

                if (tarea == null)
                {
                    ViewBag.Error = "La tarea no existe.";
                    return RedirectToAction("VerTareas");
                }

                return View("ModificarTarea", tarea); // ✅ Esta línea es la clave
            }
        [HttpPost]
        public IActionResult ModificarTareaGuardar(Tarea tarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");

            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);

            // Asegurarse de que la tarea esté vinculada al usuario actual
            tarea.IDUsuario = usuario.ID;

            if (BD.ActualizarTarea(tarea))
                return RedirectToAction("VerTareas");

            // Si falla la actualización, mostrar la vista con el error
            ViewBag.Error = "No se pudo actualizar la tarea.";
            return View("ModificarTarea", tarea);
        }
    }
}
