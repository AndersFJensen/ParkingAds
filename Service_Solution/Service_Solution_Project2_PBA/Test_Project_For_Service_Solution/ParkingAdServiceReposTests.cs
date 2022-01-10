using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Service_Solution_Project2_PBA.domain;
using Service_Solution_Project2_PBA.repositories;

namespace Test_Project_For_Service_Solution
{
    class ParkingAdServiceReposTests
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public async Task Test_Get_Parking_Data()
        {
            //Arrange
            ParkingAdServiceRepos repos = new ParkingAdServiceRepos();
            ParkingAdServiceMessageModel model = new ParkingAdServiceMessageModel();
            //Act
            model = await repos.GetParkingAdServiceDataGET();
            //Assert
            Console.WriteLine($"Service model found! Header: {model.header} Body: {model.body}\n");
            Assert.NotNull(model);
        }
    }
}
