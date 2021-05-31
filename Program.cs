using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExemploHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Clique para receber a lista de moedas");
                Console.WriteLine("Consultando o web service");

                string BaseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata";
                string action = "/Moedas?$format=json";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + action);
                HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

                List<MoedaResponse> listMoeda = new List<MoedaResponse>();

                JArray moeda = (JArray)JObject.Parse(response.Content.ReadAsStringAsync().Result)["value"];

                Console.WriteLine("quantidade: " + moeda.Count);
                Console.WriteLine("Teste: " + moeda["nomeFormatado"][0].ToString());

                /*
                for (int i = 0; i < moeda.Count; i++)
                {
                    Console.WriteLine(moeda["simbolo"][i].ToString());
                    Console.WriteLine(moeda["nomeFormatado"][i].ToString());
                    Console.WriteLine(moeda["tipoMoeda"][i].ToString());
                }
                */

                /*
                foreach (var item in listMoeda)
                {
                    listMoeda.Add(new MoedaResponse()
                    {
                        Simbolo = moeda["simbolo"].ToString(),
                        NomeFormatado = moeda["nomeFormatado"].ToString(),
                        TipoMoeda = moeda["tipoMoeda"].ToString()
                    });
                }

                Console.WriteLine("Exibindo o resultado");

                foreach (var moedas in listMoeda)
                {
                    Console.WriteLine(moedas.Simbolo);
                    Console.WriteLine(moedas.NomeFormatado);
                    Console.WriteLine(moedas.TipoMoeda);
                }
                */

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro na consulta da moeda\n" + e.Message);
            }
        }
    }
}
