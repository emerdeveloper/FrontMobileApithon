using System;
using System.Net.Http;
using System.Threading.Tasks;
using FrontMobileApithon.Models;
using FrontMobileApithon.Utilities.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using FrontMobileApithon.Models.Responses;
using System.Text;

namespace FrontMobileApithon.Services
{
    public class ApiConsumer
    {

        #region Methods
        public async Task<Response> SearchBooks(string book)
        {
            return null;
            /*try
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
            }*/
        }
        //Segundo APi
        public async Task<Response> PostGetMovements(
            string accessToken,
            string MovementsServicePrefix,
            Models.Request.Movements.RootObject model)
        {
            try
            {               
                
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.Url.BaseAdressBluemix);
                var url = MovementsServicePrefix;//string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<Models.Responses.Movements.RootObject> (result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        //Primer APi
        public async Task<Response> PostGetToken(string code)
        {
            try
            {
                /*var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                nvc.Add(new KeyValuePair<string, string>("code ", code));
                nvc.Add(new KeyValuePair<string, string>("client_id", "16ebf6cc-38f7-497f-b064-7ca1d562727a"));
                nvc.Add(new KeyValuePair<string, string>("client_secret", "xJ4wD2eL3bB2vV1yI1xR8lH7lN6hU1aJ2yH2tW6dX3gP5kS0qU"));
                nvc.Add(new KeyValuePair<string, string>("redirect_uri", "http://localhost:3000/code"));*/

                var dict = new Dictionary<string, string>();
                dict.Add("grant_type", "authorization_code");
                dict.Add("code", code);
                dict.Add("client_id", "16ebf6cc-38f7-497f-b064-7ca1d562727a");
                dict.Add("client_secret", "xJ4wD2eL3bB2vV1yI1xR8lH7lN6hU1aJ2yH2tW6dX3gP5kS0qU");
                dict.Add("redirect_uri", "http://localhost:3000/code");
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, "https://api.us.apiconnect.ibmcloud.com/bancolombiabluemix-dev/sandbox/security/oauth-otp/oauth2/token") { Content = new FormUrlEncodedContent(dict) };
                var response = await client.SendAsync(req);
                

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<GetTokenResponse>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        #endregion
    }
}
