using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP07_ROTH.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=TP6_Introducciónabasededatos;Integrated Security=True;TrustServerCertificate=True;";

        public static Integrante ObtenerPorUsuario(string user)
        {
            Integrante i = new Integrante();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE Usuario = @pUser";
                i = connection.QueryFirstOrDefault<Integrante>(query, new { pUser = user });
            }

            return i;
        }

        public static bool VerificarContraseña(string user, string contraseña)
        {
            Integrante integrante = ObtenerPorUsuario(user);
            if (integrante == null) return false;

            bool logeado = false;
            if (integrante.Contraseña == contraseña)
            {
                logeado = true;
            }

            return logeado;
        }

        public static List<Integrante> TodosLosDeUnGrupo(int Grupo) //TraerTareas
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Integrantes WHERE idGrupo = @Grupo";
return connection.Query<Integrante>(query, new { Grupo = Grupo }).ToList();
            }
        }

        public static Grupo ObtenerGrupo(int Grupo ) //TraerTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Grupos WHERE idGrupo = @idGrupo";
                return connection.QueryFirstOrDefault<Grupo>(query, new { idGrupo = Grupo });
            }
        }

        public static void AgregarIntegrante(string nombre, string usuario, string contraseña, string frase, string hobby, string profeFav, string peliculaFav, string foto, int IDgrupo) //AgregarTarea
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Integrantes(Nombre, Usuario, Contraseña, Frase, Hobby, ProfeFav, PeliculaFav, idGrupo, Foto) 
                               VALUES (@nombre, @usuario, @contraseña, @frase, @hobby, @profeFav, @peliculaFav, @IDgrupo, @foto)";

                connection.Execute(query, new{ nombre, usuario, contraseña, frase, hobby, profeFav, peliculaFav, IDgrupo, foto });
            }
        }
    }
}
