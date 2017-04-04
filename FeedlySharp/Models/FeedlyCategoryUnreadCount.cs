using Newtonsoft.Json;
using System;
using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyCategoryUnreadCount
  {
    [JsonProperty("updated")]
    public DateTime? UpdatedAt { get; set; }

    public int Count { get; set; }

    public string Id { get; set; }

    public string Name => Id == null ? string.Empty : Id.Split('/').Last();
  }
}
