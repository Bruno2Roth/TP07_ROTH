using Newtonsoft.Json;

namespace TP07_ROTH.Models
{
    public class Usuario
    {
        [JsonProperty]
        public int ID {get; set;}
        [JsonProperty]
        public string Username { get; set; }
        [JsonProperty]
        public string Password {get; set;}
        [JsonProperty]
        public string Nombre { get; set; }
        [JsonProperty]
        public string Apellido { get; set; }
        [JsonProperty]
        public string Foto { get; set; }
        [JsonProperty]
        public DateTime UltimoLogin { get; set; }
        
        public Usuario()
        {
        }
    }
}