using Newtonsoft.Json;

namespace FeedlySharp.Models
{
  public class FeedlySearchFeed : FeedlyFeed
  {
    [JsonProperty("feedId")]
    public new string Id { get; set; }
  }
}
