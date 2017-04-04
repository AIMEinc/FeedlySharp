using Newtonsoft.Json;

namespace FeedlySharp.Models
{
    public class FeedlyThumbnail
    {
        [JsonProperty("url")]
        public System.Uri Url { get; set; }
    }
}
