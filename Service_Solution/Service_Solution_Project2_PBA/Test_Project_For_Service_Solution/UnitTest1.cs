using NUnit.Framework;
using Service_Solution_Project2_PBA;
using System;

namespace Test_Project_For_Service_Solution
{
    public class Tests
    {
        public struct Entry_Version_1
        {
            public string id { get; set; }
            public string description { get; set; }
            public string owner { get; set; }
            public string timeCreated {get; set;}
            public Entry_Version_1(string id, string desc, string owner)
            {
                this.id = id;
                this.description = desc;
                this.owner = owner;
                this.timeCreated = DateTime.Now.ToString(); 
            }
        };

        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public async System.Threading.Tasks.Task Save_And_Retrieve_From_RedisAsync()
        {
            //TODO: Fix GetRecordAsync and Test! Se på --> 

            //Arrange
            string entryId = Guid.NewGuid().ToString();
            var entry = new Entry_Version_1(entryId, "This is a new entry","Mugge");
            //Act
            await DistributedCacheExtensions.SetRecordAsync(null, entry.id, entry);     //Get the cache information.
            // --> var returnedEntry = await DistributedCacheExtensions.GetRecordAsync(null, entryId);
            //Assert
            // --> Assert.AreEqual(entry, returnedEntry);
        }
    }
}