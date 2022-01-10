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
    class AdServiceReposTests
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public async Task Test_Get_AdService_Data_From_API()
        {
            //Arrange
            AdServiceServiceRepos adServiceServiceRepos = new AdServiceServiceRepos();
            AdServiceMessageModel adServiceMessageModel = null;
            //Act
            adServiceMessageModel = await adServiceServiceRepos.CallAdServiceGET();
            //Assert
            Console.WriteLine($"Service model found! Header: {adServiceMessageModel.header} Body: {adServiceMessageModel.body}\n");
            Assert.IsNotNull(adServiceMessageModel);
        }
    }
}
