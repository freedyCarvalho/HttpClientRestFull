using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExemploHttpClient 
{
    public interface IMoedaApiService
    {
        Task<List<MoedaResponse>>GetMoeda();

    }
}
