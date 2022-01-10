using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.repositories
{
    class AdServiceServiceRepos : AdServiceServiceReposIF
    {
        static HttpClient client = new HttpClient();
        private const string url = "http://psuaddservice.fenris.ucn.dk/";
        public async Task<Message> CallAdServiceGET()
        {
            Message message = null; 
            HttpResponseMessage response = await client.GetAsync(url);
            message.header = "AdService"; 
            if (response.IsSuccessStatusCode)
            {
                message.body = await response.Content.ReadAsStringAsync();
            }
            return message; 
        }
    }
}
