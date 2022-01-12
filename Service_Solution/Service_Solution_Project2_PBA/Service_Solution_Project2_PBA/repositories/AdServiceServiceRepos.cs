using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.repositories
{
    public class AdServiceServiceRepos : AdServiceServiceReposIF
    {
        private static HttpClient client = new HttpClient();
        private const string url = "http://psuaddservice.fenris.ucn.dk/";

        public AdServiceServiceRepos()
        {
            client.BaseAddress = new Uri("http://localhost:64195/"); 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AdServiceMessageModel> CallAdServiceGET()
        {
            AdServiceMessageModel message = new AdServiceMessageModel();
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                message.header = "AdService";
                if (response.IsSuccessStatusCode)
                {
                    message.body = await response.Content.ReadAsStringAsync();
                }
            }

            return message; 
        }
    }
}
