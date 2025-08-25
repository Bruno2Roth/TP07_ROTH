using Newtonsoft.Json;

namespace TP07_ROTH.Models
{
    public class Tarea
    {
        [JsonProperty]
        public int ID { get; set; }
        [JsonProperty]
        public int IDUsuario { get; set; }
        [JsonProperty]
        public string Titulo { get; set; }
        [JsonProperty]
        public string Descripcion { get; set; }
        [JsonProperty]
        public DateTime Fecha { get; set; }
        [JsonProperty]
        public bool Finalizada { get; set; }
        [JsonProperty]
        public bool Eliminada { get; set; }

        public Tarea()
        {
        }
        public Tarea(int pID, int pIDUsuario, string pTitulo, string pDescripcion, DateTime pFecha, bool pFinalizada, bool pEliminada)
        {
            ID = pID;
            IDUsuario = pIDUsuario;
            Titulo = pTitulo;
            Descripcion = pDescripcion;
            Fecha = pFecha;
            Finalizada = pFinalizada;
            Eliminada = pEliminada;
        }

    }
}