namespace FrontMobileApithon.Models
{
    using System;

    public class Response
    {
        #region Constructors
        public Response()
        {
        }
        #endregion

        #region Properties
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
        #endregion
    }
}
