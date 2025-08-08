using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP07_ROTH.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=TP6_Introducciónabasededatos;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario ObtenerPorUsuario(string user)
        {
            Usuario i = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE Usuario = @pUser";
                i = connection.QueryFirstOrDefault<Usuario>(query, new { pUser = user });
            }

            return i;
        }

        public static bool VerificarContraseña(string user, string password)
        {
            Usuario username = ObtenerPorUsuario(user);
            if (username == null) return false;

            bool logeado = false;
            if (username.Password == password)
            {
                logeado = true;
            }

            return logeado;
        }

        public static List<Usuario> TraerTareas(int Tareas) //TraerTareas
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE IDTareas = @Tareas";
                return connection.Query<Usuario>(query, new { Tareas = Tareas }).ToList();
            }
        }

        public static Tarea TraerTarea(int Tareas) //TraerTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Grupos WHERE IDTarea = @IDTarea";
                return connection.QueryFirstOrDefault<Tarea>(query, new { ID = Tareas });
            }
        }

        public static void Registro(Usuario usuario) //Registro
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF (SELECT 1 FROM Grupos WHERE Ussername = @usuario.Ussername)";
                if (query != "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"INSERT INTO Usuarios(ID, Usuario, Contraseña, Nombre, Apellido, Foto, UltimoLogin)
                               VALUES (@usuario.ID, @usuario.Ussername, @usuario.Password, @usuario.Nombre, @usuario.Apellido, @usuario.Foto, @usuario.UltimoLogin)";

                        connection1.Execute(query, new { usuario.ID, usuario.Ussername, usuario.Password, usuario.Nombre, usuario.Apellido, usuario.Foto });
                    }
                }
                else
                {
                    //mensaje de que el usuario ya esta ocupado
                }
            }
        }
        public static void CrearTarea(Tarea tarea) //CrearUsuario
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF (SELECT 1 FROM Grupos WHERE Titulo = @tarea.Titulo)";
                if (query != "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"INSERT INTO Tareas(tarea.ID, tarea.IDUsuario, tarea.Titulo, tarea.Descripcion, tarea.Fecha, tarea.Finalizada, tarea.Eliminada) 
                               VALUES (@tarea.ID, @IDUsuario, @Titulo, @Descripcion, @Fecha, @Finalizada, @Eliminada)";

                        connection.Execute(query, new { tarea.ID, tarea.IDUsuario, tarea.Titulo, tarea.Descripcion, tarea.Fecha, tarea.Finalizada, tarea.Eliminada });
                    }
                }
                else
                {
                    //mensaje de que el nombre de la tarea ya esta en uso
                }
            }
        }
        public static void EliminarTarea(int IDdelaTarea) //EliminarTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF (SELECT 1 FROM Grupos WHERE Ussername = @usuario.Ussername)";
                if (query == "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"UPDATE Tareas SET Tareas.Eliminada = 1 WHERE Tareas.ID = @IDdelaTarea";
                    }
                }
            }
        }
    }
}
