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
    }
}