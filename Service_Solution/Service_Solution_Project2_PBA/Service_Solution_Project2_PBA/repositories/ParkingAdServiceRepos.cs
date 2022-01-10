using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.repositories
{
    public class ParkingAdServiceRepos : ParkingAdServiceReposIF
    {
        private static HttpClient client = new HttpClient();
        private readonly string url = "http://psuparkingservice.fenris.ucn.dk/";

        public ParkingAdServiceRepos()
        {
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ParkingAdServiceMessageModel> GetParkingAdServiceDataGET()
        {
            ParkingAdServiceMessageModel message = new ParkingAdServiceMessageModel();
            HttpResponseMessage response = await client.GetAsync(url);
            message.header = "ParkingAdService message!";
            if (response.IsSuccessStatusCode)
            {
                message.body = await response.Content.ReadAsStringAsync();
            }

            return message; 
        }
    }
}
