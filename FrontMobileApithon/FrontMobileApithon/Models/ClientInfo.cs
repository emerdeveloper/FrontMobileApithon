using System;
namespace FrontMobileApithon.Models
{
    public class ClientInfo
    {
        public ClientInfo()
        {

        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cellPhone { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public bool isUpdated { get; set; }
        public bool declarationReady { get; set; }
        public string pdf { get; set; }
        public bool updated { get; set; }
        public string fullName { get; set; }
    }
}
