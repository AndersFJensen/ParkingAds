using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service_Solution_Project2_PBA.domain
{
    [Serializable]
    public class ParkingAdServiceMessageModel
    {
        [JsonInclude]
        public string header { get; set; }
        [JsonInclude]
        public string body { get; set; }
    }
}
