using System.Collections.Generic;

namespace FeedlySharp.Models
{
  public class FeedlySearchResponse
  {
    public string Continuation { get; set; }

    public string Title { get; set; }

    public string Id { get; set; }

    public List<FeedlyEntry> Items { get; set; }
  }
}
