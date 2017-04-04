using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
    public class FeedlyVisual
    {

        [JsonProperty("url")]
        public Uri Uri { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
