using System;
using System.Net.Http;
using System.Threading.Tasks;
using FrontMobileApithon.Models;
using FrontMobileApithon.Utilities.Enums;
using Newtonsoft.Json;

namespace FrontMobileApithon.Services
{
    public class ApiConsumer
    {
        #region Methods
        public async Task<Response> SearchBooks(string book)
        {
            try
            {

                HttpClient httpClient = new HttpClient
                {

                    BaseAddress = new Uri(Constants.Url.BaseAdress)
                };

                var response = await httpClient.GetAsync(string.Format("{0}{1}/{2}", Constants.Url.BaseAdressApi, Constants.Url.Search, book));

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Constants.Messages.ErrorResponse,
                        Result = null,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var resultObject = JsonConvert.DeserializeObject<SearchRequest>(result);

                if (!resultObject.Error.Equals("0"))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Constants.Messages.ErrorResponse,
                        Result = null,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = Constants.Status.SuccessResponse,
                    Result = resultObject.Books,
                };

            }
            catch (Exception e)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("::::Class : ApiConsumer----- Method: SearchBooks(string book)" + e.Message.ToString());
#endif
                return new Response
                {
                    IsSuccess = false,
                    Message = Constants.Status.ErrorResponse,
                    Result = null,
                };
            }
        }
        #endregion
    }
}
