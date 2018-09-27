namespace FrontMobileApithon.Models
{
    using System;
    using Newtonsoft.Json;

    public class Book
    {
        #region Constructors
        public Book()
        {
        }
        #endregion

        #region Properties
        [JsonProperty(PropertyName = "ID")]
        public object Id { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "SubTitle")]
        public string SubTitle { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Image")]
        public string Image { get; set; }


        [JsonProperty(PropertyName = "isbn")]
        public string Isbn { get; set; }
        #endregion
    }
}
