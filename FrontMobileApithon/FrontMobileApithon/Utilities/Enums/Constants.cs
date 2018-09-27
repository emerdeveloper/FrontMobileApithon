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
            public const string BaseAdress = "http://it-ebooks-api.info";
            public const string BaseAdressApi = "/v1/";
            public const string Search = "search";
            public const string Book = "book";
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

        public struct Activity
        {
            public const string Books = "Libros";
        }
    }
}
