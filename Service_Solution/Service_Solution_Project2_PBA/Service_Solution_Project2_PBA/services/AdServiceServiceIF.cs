using Service_Solution_Project2_PBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.services
{
    public interface AdServiceServiceIF
    {
        public Task<Message> GetAdFromAdService();
    }
}
