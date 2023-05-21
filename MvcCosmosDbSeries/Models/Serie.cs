using Newtonsoft.Json;

namespace MvcCosmosDbSeries.Models
{
    public class Serie
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int Puntuacion { get; set; }
        public int Anyo { get; set; }
    }
}
