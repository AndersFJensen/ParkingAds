using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;

namespace Service_Solution_Project2_PBA.repositories
{
    public class ParkingAdServiceRepos : ParkingAdServiceReposIF
    {
        private static HttpClient client = new HttpClient();
        private static readonly string url = "http://psuparkingservice.fenris.ucn.dk/service";
        private static Timer aTimer;


        public ParkingAdServiceRepos()
        {
            if(client is null)
            {
                client.BaseAddress = new Uri("http://localhost:64196/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
            
        }
        public async Task<ParkingAdServiceMessageModel> GetParkingAdServiceDataGET()
        {
            ParkingAdServiceMessageModel message = new ParkingAdServiceMessageModel();
            SetTimer();
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                aTimer.Stop();
                message.header = "ParkingAdService message!";
                if (response.IsSuccessStatusCode)
                {
                    message.body = await response.Content.ReadAsStringAsync();
                }
            }
            aTimer.Dispose(); 
            return message; 
        }

        private static void SetTimer()
        {
            aTimer = new Timer(2000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            Console.Error.Write("Time for respond took to long. Read from cache!");
            //Should have a method for the latter.
        }
    }
}
