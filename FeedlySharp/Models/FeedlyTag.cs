using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyTag
  {
    public string Id { get; set; }

    public string Name => Id == null ? string.Empty : Id.Split('/').Last();

      public string Label { get; set; }

    public bool IsGlobal => Name.StartsWith("global.");
  }
}
