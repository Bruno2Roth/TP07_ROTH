using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP07_ROTH.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=TP6_Introducciónabasededatos;Integrated Security=True;TrustServerCertificate=True;";

        public static bool VerificarContraseña(string user, string password) //verficar contraseña
        {
            Usuario x = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE user = @ussername";
                x = connection.QueryFirstOrDefault<Usuario>(query, new { ussername = user });
            }
            if (x == null) return false;

            bool logeado = false;
            if (x.Password == password)
            {
                logeado = true;
            }

            return logeado;
        }

        public static List<Usuario> TraerTareas(int IDusuario) //TraerTareas
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE IDTareas = @tareas";
                return connection.Query<Usuario>(query, new { tareas = IDusuario }).ToList();
            }
        }

        public static Tarea TraerTarea(int IDtarea) //TraerTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Grupos WHERE IDTarea = @IDTarea";
                return connection.QueryFirstOrDefault<Tarea>(query, new { IDTarea = IDtarea });
            }
        }

        public static void Registro(Usuario usuario) //Registro
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF EXISTS(SELECT 1 FROM Usuarios WHERE Ussername = @Ussername)";
                if (query != "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"INSERT INTO Usuarios(ID, Usuario, Contraseña, Nombre, Apellido, Foto, UltimoLogin)
                               VALUES (@ID, @Ussername, @Password, @Nombre, @Apellido, @Foto, @UltimoLogin)";

                        connection1.Execute(query1, new { usuario.ID, usuario.Ussername, usuario.Password, usuario.Nombre, usuario.Apellido, usuario.Foto });
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
                string query = "IF EXISTS(SELECT 1 FROM Tareas WHERE Titulo = @Titulo)";
                if (query != "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"INSERT INTO Tareas(ID, IDUsuario, Titulo, Descripcion, Fecha, Finalizada, Eliminada) 
                               VALUES (@ID, @IDUsuario, @Titulo, @Descripcion, @Fecha, @Finalizada, @Eliminada)";

                        connection.Execute(query, new { tarea.ID, tarea.IDUsuario, tarea.Titulo, tarea.Descripcion, tarea.Fecha, tarea.Finalizada, tarea.Eliminada });
                    }
                }
                else
                {
                    //mensaje de que el nombre de la tarea ya esta en uso
                }
            }
        }
        public static void EliminarTarea(int IDtarea) //EliminarTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF EXISTS(SELECT 1 FROM Grupos WHERE IDTarea = @usuario.IDTarea)";
                if (query == "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"UPDATE Tareas SET Tareas.Eliminada = 1 WHERE Tareas.ID = @IDdelaTarea"; //falta ver que actualice el del usuario especificado
                    }
                }
                else
                {
                    //mensaje de que la tarea no existe
                }
            }
        }
        public static void ActualizarTarea(Tarea tarea) //ActualizarTarea

        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "IF EXISTS(SELECT 1 FROM Tareas WHERE ID = @tarea.ID)";
                if (query == "1")
                {
                    using (SqlConnection connection1 = new SqlConnection(_connectionString))
                    {
                        string query1 = @"UPDATE Tareas SET Eliminada = 1 WHERE ID = @tarea.ID";
                    }
                }
                else
                {
                    //mensaje de que la tarea no existe
                }
            }
        }
        public static void FinalizarTarea(int IDtarea)
        {

        }
        public static void ActualizarFechaLogin(int IDusuario)
        {

        }
    }
}
