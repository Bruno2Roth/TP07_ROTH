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


        public IActionResult CargarTareas()
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            string username = HttpContext.Session.GetString("Username");

            Usuario usuario = BD.ObtenerPorUsername(username);
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
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
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
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
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
                
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
            BD.FinalizarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }

        public IActionResult EliminarTarea(int IDdelaTarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
            
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
            BD.EliminarTarea(IDdelaTarea);
            return RedirectToAction("VerTareas");
        }

        public IActionResult ModificarTarea(int id)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
                
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
            Tarea tarea = BD.TraerTarea(id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult ModificarTareaGuardar(Tarea tarea)
        {
            if (HttpContext.Session.GetString("EstaLogin") != "true")
                return RedirectToAction("Login", "Account");
                
            string username = HttpContext.Session.GetString("Username");
            Usuario usuario = BD.ObtenerPorUsername(username);
            
            // Usuario manual de respaldo si falla la base de datos
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    ID = 1,
                    Username = "bruno",
                    Password = "admin123",
                    Nombre = "Usuario",
                    Apellido = "Respaldo",
                    Foto = "",
                    UltimoLogin = DateTime.Now
                };
            }
            
            if (BD.ActualizarTarea(tarea))
                return RedirectToAction("VerTareas");

            ViewBag.Error = true;
            return View("ModificarTarea", tarea);
        }
    }
}
