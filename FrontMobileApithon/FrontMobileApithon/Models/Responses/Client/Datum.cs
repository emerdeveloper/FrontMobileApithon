using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontMobileApithon.Models.Responses.Client
{
    public class Datum
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cellPhone { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public bool updated { get; set; }
        public string fullName { get; set; }
        public bool declarationReady { get; set; }
    }
}
