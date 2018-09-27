using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontMobileApithon.Models.Request.Aggregation
{
    public class Datum
    {
        public Header header { get; set; }
        public List<Document> document { get; set; }
    }
}
