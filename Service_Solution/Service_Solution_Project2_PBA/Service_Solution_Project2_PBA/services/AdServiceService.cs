using Service_Solution_Project2_PBA.domain;
using Service_Solution_Project2_PBA.repositories;
using System;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.services
{
    class AdServiceService : AdServiceServiceIF
    {
        public async Task<AdServiceMessageModel> GetAdFromAdService()
        {
            AdServiceServiceRepos adServiceServiceRepos = new AdServiceServiceRepos();

            return await adServiceServiceRepos.CallAdServiceGET();
        }
    }
}
