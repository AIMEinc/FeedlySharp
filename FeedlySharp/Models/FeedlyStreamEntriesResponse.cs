using System.Collections.Generic;

namespace FeedlySharp.Models
{
  public class FeedlyStreamEntriesResponse
  {
    public string Continuation { get; set; }

    public List<FeedlyEntry> Items { get; set; }
  }
}
