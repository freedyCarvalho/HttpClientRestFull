using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExemploHttpClient
{
    class Program
    {

        // Consulta: H:\Dropbox (MKII Corporation)\Capta Tecnologia\Moedas\
        // Consulta: H:\Dropbox (MKII Corporation)\Capta Tecnologia\teste-unitario\

        private static readonly int opcao = 1;

        static void Main(string[] args)
        //static async Task Main(string[] args)
        {
            try
            {

                if (Program.opcao == 1)
                {
                    Console.WriteLine("Clique para receber a lista de moedas");
                    Console.WriteLine("Consultando o web service");

                    HttpClient client = new HttpClient();
                    string BaseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata";
                    string action = "/Moedas?$format=json";

                    var dados = RetornaDados(BaseUrl + action, client).Result;

                    var moeda = JsonConvert.DeserializeObject<MoedaResponse>(dados);

                    var listMoeda = moeda.value;

                    foreach (var item in listMoeda)
                    {
                        Console.WriteLine("Símbolo: " + item.Simbolo);
                        Console.WriteLine("Nome Formatado: " + item.NomeFormatado);
                        Console.WriteLine("Tipo Moeda: " + item.TipoMoeda);
                        Console.WriteLine("");
                    }
                }
                else if (Program.opcao == 2)
                {
                    Console.WriteLine("Clique para receber a lista de moedas");
                    Console.WriteLine("Consultando o web service");

                    string BaseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata";
                    string action = "/Moedas?$format=json";

                    var dados = ProcessRepositories(BaseUrl, action).Result;
                    var moeda = JsonConvert.DeserializeObject<MoedaResponse>(dados);
                    var listMoeda = moeda.value;

                    Console.WriteLine("Mostrando os resultados:");

                    foreach (var item in listMoeda)
                    {
                        Console.WriteLine("Símbolo: " + item.Simbolo);
                        Console.WriteLine("Nome Formatado: " + item.NomeFormatado);
                        Console.WriteLine("Tipo Moeda: " + item.TipoMoeda);
                        Console.WriteLine("");
                    }
                }
                else if (Program.opcao == 3)
                {
                    Console.WriteLine("Clique para receber a lista de moedas");
                    Console.WriteLine("Consultando o web service");

                    string BaseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata";
                    string action = "/Moedas?$format=json";

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + action);
                    HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

                    var moeda = JsonConvert.DeserializeObject<MoedaResponse>(response.Content.ReadAsStringAsync().Result);
                    var listMoeda = moeda.value;

                    foreach (var item in listMoeda)
                    {
                        Console.WriteLine("Símbolo: " + item.Simbolo);
                        Console.WriteLine("Nome Formatado: " + item.NomeFormatado);
                        Console.WriteLine("Tipo Moeda: " + item.TipoMoeda);
                        Console.WriteLine("");
                    }

                    //JArray moeda = (JArray)JObject.Parse(response.Content.ReadAsStringAsync().Result)["@odata.context"];
                }
                else
                {
                    Console.WriteLine("Clique para receber a lista de moedas");
                    Console.WriteLine("Consultando o web service");

                    string BaseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata";
                    string action = "/Moedas?$format=json";

                    var dados = ConsultarMoeda(BaseUrl + action).Result;

                    var moeda = JsonConvert.DeserializeObject<MoedaResponse>(dados);
                    var listMoeda = moeda.value;

                    foreach (var item in listMoeda)
                    {
                        Console.WriteLine("Símbolo: " + item.Simbolo);
                        Console.WriteLine("Nome Formatado: " + item.NomeFormatado);
                        Console.WriteLine("Tipo Moeda: " + item.TipoMoeda);
                        Console.WriteLine("");
                    }
                }

               Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta da moeda\n" + e.Message);
            }
        }


        /*
        private static async Task<List<MoedaResponse>> ProcessRepositories(string url, string action)
        {
            //throw new NotImplementedException();

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("accept", "application/json;odata.metadata=minimal");

            var streamTask = client.GetStreamAsync($"{url}" + action);
            var moedas = await JsonSerializer.DeserializeAsync<List<MoedaResponse>>(await streamTask, options);
            return moedas;
        }
        */


        #region Opcao 1
        private static async Task<string> RetornaDados(string url, HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsStringAsync();
            return result;
        }
        #endregion Opcao 1

        #region Opcao 2
        private static async Task<string> ProcessRepositories(string url, string action)
        {
            //throw new NotImplementedException();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("accept", "application/json;odata.metadata=minimal");

            var stringTask = client.GetStringAsync(url + action);
            var msg = await stringTask;
            return msg;
        }
        #endregion Opcao 2

        #region Opcao 4
        private static async Task<string> ConsultarMoeda(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var retorno = await response.Content.ReadAsStringAsync();

            return retorno;
        }
        #endregion Opcao 4
    }
}
