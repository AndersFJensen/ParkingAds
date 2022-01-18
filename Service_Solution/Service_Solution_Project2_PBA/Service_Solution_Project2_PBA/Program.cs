using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading;
using Service_Solution_Project2_PBA.services;
using System.Timers;

namespace Service_Solution_Project2_PBA
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        public static AdServiceServiceIF MyAdService { get; set; }

        [Serializable]
        private struct TestEntry
        {
            [JsonInclude]
            public string id;
            [JsonInclude]
            public string desc; 
            public TestEntry(string id, string desc)
            {
                this.id = id;
                this.desc = desc; 
            }
        }

        static void Main(string[] args)
        {
            //TODO: Benchmark with alot of entries without cache. 
            //Test with cache. 
            //Test with multiple server clients. 
            //Q1: "What should we do if the distributed cache are overqueried?"
            //Split code into seperate files. (Very importanté!).
            //Communicate with ParkingAd and ParkingService. (Very importanté!).
            MyAdService = new AdServiceService();

            Init();
            SetTimer();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(120000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MyAdService.UpdateAdInCache();
        }

        private async static void Init()
        {
            //List<TestEntry> testList = await TestRedisCacheConnection();

            var rabbitMQReciever = new RabbitMQRecieve();
            //rabbitMQReciever.RabbitReceiveServiceSolution;
            
            //foreach (TestEntry entry in testList)
            //{
            //    var rabbitMQSent = new RabbitMQSent(entry.ToString());
            //}
        }

        //private static async Task<List<TestEntry>> TestRedisCacheConnection()
        //{
        //    RedisCacheService redisCacheService = new RedisCacheService();
            
        //    var TestEntry1 = new TestEntry("123", "It is working!");
        //    var TestEntry2 = new TestEntry("1234", "It is workingx!");
        //    var TestEntry3 = new TestEntry("12345", "It is workingy!");
        //    var TestEntry4 = new TestEntry("123456", "It is workingz!");
        //    await redisCacheService.SaveToCache(TestEntry1.id, TestEntry1);
        //    await redisCacheService.SaveToCache(TestEntry2.id, TestEntry2);
        //    await redisCacheService.SaveToCache(TestEntry3.id, TestEntry3);
        //    await redisCacheService.SaveToCache(TestEntry4.id, TestEntry4);
        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //    List<TestEntry> list = new List<TestEntry>();
        //    list.Add(await redisCacheService.RetrieveFromCache<TestEntry>(TestEntry1.id));
        //    list.Add(await redisCacheService.RetrieveFromCache<TestEntry>(TestEntry2.id));
        //    list.Add(await redisCacheService.RetrieveFromCache<TestEntry>(TestEntry3.id));
        //    list.Add(await redisCacheService.RetrieveFromCache<TestEntry>(TestEntry4.id));
        //    if (list.Count != 0)
        //        foreach (TestEntry result in list)
        //        {
        //            Console.WriteLine($"Entry with {result.id} and discription '{result.desc}' was found! \n");
        //        }
        //    else
        //    { Console.WriteLine("Not working!"); }
        //    return list;
        //}
    }
}
