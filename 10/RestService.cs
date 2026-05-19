using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace pract10
{
    public class HttpClientHandlerInsecure : HttpClientHandler
    {
        public HttpClientHandlerInsecure()
        {
            ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;
        }
    }

    public class RestService
    {
        private static readonly HttpClient httpClient =
            new HttpClient(new HttpClientHandlerInsecure())
            {
                BaseAddress = new Uri("https://localhost:7170/api/")
            };

        public async Task<List<Experiment>> GetExperimentsAsync()
        {
            return await httpClient
                .GetFromJsonAsync<List<Experiment>>("Experiments");
        }

        public async Task<Experiment> CreateExperimentAsync(
            Experiment experiment)
        {
            using (HttpResponseMessage response =
                await httpClient.PostAsJsonAsync(
                    "Experiments",
                    experiment))
            {
                return await response.Content
                    .ReadFromJsonAsync<Experiment>();
            }
        }

        public async Task DeleteExperimentAsync(int id)
        {
            using (HttpResponseMessage response =
                await httpClient.DeleteAsync($"Experiments/{id}"))
            {
                await response.Content
                    .ReadAsStringAsync();
            }
        }
    }
}
