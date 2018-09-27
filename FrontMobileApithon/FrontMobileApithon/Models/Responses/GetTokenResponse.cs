using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontMobileApithon.Models.Responses
{
    class GetTokenResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int consented_on { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
        public int refresh_token_expires_in { get; set; }
    }
}
