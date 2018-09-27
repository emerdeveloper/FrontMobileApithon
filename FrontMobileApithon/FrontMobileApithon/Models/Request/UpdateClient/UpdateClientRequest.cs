using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontMobileApithon.Models.Request.UpdateClient
{
    public class UpdateClientRequest
    {
        public List<Datum> data { get; set; }
        public string _id { get; set; }
        public string _rev { get; set; }
    }
}
