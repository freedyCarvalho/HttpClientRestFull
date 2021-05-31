using Newtonsoft.Json;

namespace ExemploHttpClient
{
    public class MoedaResponse
    {
        [JsonProperty("simbolo")]
        public string Simbolo { get; set; }

        [JsonProperty("nomeFormatado")]
        public string NomeFormatado { get; set; }

        [JsonProperty("tipoMoeda")]
        public string TipoMoeda { get; set; }

    }
}
