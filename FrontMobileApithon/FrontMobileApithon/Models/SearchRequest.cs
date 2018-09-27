namespace FrontMobileApithon.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SearchRequest
    {
        #region Constructors
        public SearchRequest()
        {
        }
        #endregion

        #region Properties
        [JsonProperty(PropertyName = "Error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "Time")]
        public double Time { get; set; }

        [JsonProperty(PropertyName = "Total")]
        public string Total { get; set; }

        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "Books")]
        public List<Book> Books { get; set; }
        #endregion
    }
}
