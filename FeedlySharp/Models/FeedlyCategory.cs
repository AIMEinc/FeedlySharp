using Newtonsoft.Json;
using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyCategory
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    public string Name => Id == null ? string.Empty : Id.Split('/').Last();

      [JsonProperty("label")]
    public string Label { get; set; }

    public bool IsGlobal => Name.StartsWith("global.");
  }
}
