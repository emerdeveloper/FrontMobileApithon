using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontMobileApithon.Utilities.Enums
{
    public class Constants
    {
        public struct Url
        {
            public const string BaseAdress = "https://api.us.apiconnect.ibmcloud.com/bancolombiabluemix-dev";
            public const string TokenSecurity = "/sandbox/security/oauth-otp/oauth2/token";

            public const string BaseAdressBluemix = "http://173.193.75.209:";
            public const string MovementsServicePrefix = "32061/getMovements";
            public const string GetClientServicePrefix = "31333/getClient";
            public const string UpdateClientInfoServicePrefix = "31333/updateClient";
            public const string AggregationServicePrefix = "31864/aggregation";
        }

        public struct Messages
        {
            public const string TurnInternetConnection = "Por favor encienda su conección a internet";
            public const string CheckInternetConnection = "Por favor verifique su conección a internet";
            public const string ErrorResponse = "Ocurrió un error inesperado";
            public const string Initial = "Buscador de libros";
            public const string NotFound = "No hay resultados para \"{0}\"";
        }

        public struct Status
        {
            public const string SuccessResponse = "OK";
            public const string ErrorResponse = "Error";
        }
    }
}
