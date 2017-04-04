using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FeedlySharp.Extensions
{
  internal static class UriExtensions
  {
    internal static Dictionary<string, string> ParseQueryString(this Uri uri)
    {
      var uriString = uri.OriginalString;
      var substring = uriString.Substring(((uriString.LastIndexOf('?') == -1) ? 0 : uriString.LastIndexOf('?') + 1));

      var pairs = substring.Split('&');

        return pairs.Select(piece => piece.Split('=')).ToDictionary(pair => pair[0], pair => WebUtility.UrlDecode(pair[1]));
    }


    internal static string ToQueryString(this IDictionary<string, string> dict)
    {
      if (dict.Count == 0) return string.Empty;

      var buffer = new StringBuilder();
      var count = 0;
      var end = false;

      foreach (var key in dict.Keys)
      {
        if (string.IsNullOrWhiteSpace(dict[key]))
        {
          continue;
        }

        var value = WebUtility.UrlEncode(dict[key]);

        if (count == dict.Count - 1)
        {
          end = true;
        }

          buffer.AppendFormat(end ? "{0}={1}" : "{0}={1}&", key, value);

          count++;
      }

      return buffer.ToString();
    }
  }
}
