using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExemploHttpClient
{
    public class MoedaResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<Value> value { get; set; }
        public class Value
        {
            [JsonProperty("simbolo")]
            public string Simbolo { get; set; }

            [JsonProperty("nomeFormatado")]
            public string NomeFormatado { get; set; }

            [JsonProperty("tipoMoeda")]
            public string TipoMoeda { get; set; }
        }

    }
}
