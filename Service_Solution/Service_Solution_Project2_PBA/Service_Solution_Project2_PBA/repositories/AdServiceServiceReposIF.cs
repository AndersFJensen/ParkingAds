using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.repositories
{
    public interface AdServiceServiceReposIF
    {
        public Task<AdServiceMessageModel> CallAdServiceGET();
    }
}
