using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP07_ROTH.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=TP07_ROTH;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario ObtenerPorUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Username = @Username";
                return connection.QueryFirstOrDefault<Usuario>(query, new { Username = username });
            }
        }

        public static bool VerificarContraseña(string Username, string Password)
        {
            Usuario usuario = ObtenerPorUsername(Username);

            if (usuario == null || usuario.Password != Password)
            {
                return false;
            }
            else return true;
        }

        public static List<Tarea> TraerTareas(int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE IDUsuario = @IDUsuario AND Eliminada = 0";
                return connection.Query<Tarea>(query, new { IDUsuario = IDusuario }).ToList();
            }
        }


        public static Tarea TraerTarea(int IDTarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE ID = @ID AND Eliminada = 0";
                return connection.QueryFirstOrDefault<Tarea>(query, new { ID = IDTarea });
            }
        }

        public static bool Registro(Usuario usuario) //Registro
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Console.WriteLine("Username recibido: " + usuario.Username);

                string QueryExiste = "SELECT 1 FROM Usuarios WHERE Username = @Username";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { Username = usuario.Username });
                if (existe != 1)
                {
                    usuario.UltimoLogin = DateTime.Now;
                    string query = @"INSERT INTO Usuarios(Username, Password, Nombre, Apellido, Foto, UltimoLogin)
                               VALUES (@Username, @Password, @Nombre, @Apellido, @Foto, @UltimoLogin)";

                    connection.Execute(query, new { usuario.Username, usuario.Password, usuario.Nombre, usuario.Apellido, usuario.Foto, usuario.UltimoLogin });
                    return true; //mensaje de que el usuario se registro
                }
                else
                {
                    return false; //mensaje de que el usuario ya esta en uso
                }
            }
        }
        public static bool CrearTarea(Tarea tarea) //CrearTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string QueryExiste = "SELECT 1 FROM Tareas WHERE Titulo = @Titulo";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { tarea.Titulo });

                if (existe != 1)// no existe una tarea con ese nombre
                {
                    string query = @"INSERT INTO Tareas(IDUsuario, Titulo, Descripcion, Fecha, Finalizada, Eliminada) 
                               VALUES (@IDUsuario, @Titulo, @Descripcion, @Fecha, @Finalizada, @Eliminada)";
                    connection.Execute(query, new { tarea.IDUsuario, tarea.Titulo, tarea.Descripcion, tarea.Fecha, tarea.Finalizada, tarea.Eliminada });

                    return true; // tarea creada 
                }
                else
                {
                    return false; // ya existe una tarea con ese título
                }
            }
        }

        public static bool EliminarTarea(int IDdelaTarea) //EliminarTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string QueryExiste = "SELECT 1 FROM Tareas WHERE ID = @IDdelaTarea";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { IDdelaTarea });
                if (existe == 1)
                {
                    string query = @"UPDATE Tareas SET Tareas.Eliminada = 1 WHERE Tareas.ID = @IDdelaTarea";
                    connection.Execute(query, new { IDdelaTarea = IDdelaTarea });
                    return true; //"se elimino""
                }
                else
                {
                    return false; //"no se elimino"
                }
            }
        }
        public static bool ActualizarTarea(Tarea tarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string QueryExiste = "SELECT 1 FROM Tareas WHERE ID = @ID AND Eliminada = 0";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { ID = tarea.ID });

                if (existe == 1)
                {
                    string query = @"UPDATE Tareas SET Titulo = @Titulo, Descripcion = @Descripcion, Fecha = @Fecha, Finalizada = @Finalizada, Eliminada = @Eliminada WHERE ID = @ID";

                    connection.Execute(query, new { tarea.Titulo, tarea.Descripcion, tarea.Fecha, tarea.Finalizada, tarea.Eliminada, tarea.ID });

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public static bool FinalizarTarea(int IDdelaTarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string QueryExiste = "SELECT 1 FROM Tareas WHERE ID = @ID AND Eliminada = 0";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { ID = IDdelaTarea });

                if (existe == 1)
                {
                    string query = @"UPDATE Tareas SET Finalizada = 1 WHERE ID = @ID";
                    connection.Execute(query, new { ID = IDdelaTarea });
                    return true; //finalizada
                }
                else
                {
                    return false; //no existe
                }
            }
        }

        public static void ActualizarFechaLogin(int IDdelUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string QueryExiste = "SELECT 1 FROM Usuarios WHERE ID = @ID";
                int existe = connection.QueryFirstOrDefault<int>(QueryExiste, new { ID = IDdelUsuario });

                if (existe == 1)
                {
                    DateTime FechaActual = DateTime.Now;
                    string query = @"UPDATE Usuarios SET UltimoLogin = @FechaActual WHERE ID = @ID";
                    connection.Execute(query, new { FechaActual, ID = IDdelUsuario });
                }
            }
        }
        public static string Mensaje(string mensaje)
        {
            return mensaje;
        }

    }
}
