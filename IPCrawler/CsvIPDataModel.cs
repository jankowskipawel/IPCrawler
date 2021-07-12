using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCrawler
{
    class CsvIPDataModel
    {
        public string Ip { get; set; }
        public string Geolocation { get; set; }
        public string ISP { get; set; }
        public int NumberOfHits { get; set; }

        public CsvIPDataModel(string ip, string geolocation, string isp, int numberOfHits)
        {
            Ip = ip;
            Geolocation = geolocation;
            ISP = isp;
            NumberOfHits = numberOfHits;
        }
    }
}
