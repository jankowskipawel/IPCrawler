using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCrawler
{
    public class IPData
    {
        public string status { get; set; }
        public string country { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string isp { get; set; }
        public string query { get; set; }
    }

}
