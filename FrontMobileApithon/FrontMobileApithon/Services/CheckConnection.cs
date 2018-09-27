namespace eBooks.Services
{
    using System;
    using System.Threading.Tasks;
    using FrontMobileApithon.Models;
    using FrontMobileApithon.Utilities.Enums;
    using Plugin.Connectivity;

    public class CheckConnection
    {
        #region Constructors
        public CheckConnection()
        {
        }
        #endregion

        #region Methods
        public async Task<Response> Check()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Constants.Messages.TurnInternetConnection,
                };
            }

            var isRechable = await CrossConnectivity.Current.IsRemoteReachable(Constants.Url.BaseAdress);

            if (!isRechable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Constants.Messages.CheckInternetConnection,
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = Constants.Status.SuccessResponse,
            };
        }
        #endregion
    }
}
