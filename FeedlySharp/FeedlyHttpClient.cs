using FeedlySharp.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  internal class FeedlyHttpClient : HttpClient
  {
    public string AccessToken { get; set; }


    public FeedlyHttpClient(Uri baseUri)
    {
      BaseAddress = baseUri;
      //DefaultRequestHeaders.Add("Accept", "application/json");
    }


    public async Task<T> Request<T>(
      HttpMethod method, 
      string requestUri, 
      object body = null, 
      bool bodyAsJson = false, 
      bool isOauth = true,
      CancellationToken cancellationToken = default(CancellationToken)
    ) where T : class, new()
    {
      var responseString = await Request(method, requestUri, body, bodyAsJson, isOauth, cancellationToken).ConfigureAwait(false);

      if (responseString == "[]")
      {
        return new T();
      }
      return (new[] { "", "{}" }).Contains(responseString) ? null : DeserializeJson<T>(responseString);
    }


    public async Task<string> Request(
      HttpMethod method, 
      string requestUri, 
      object body = null, 
      bool bodyAsJson = false, 
      bool isOauth = true,
      CancellationToken cancellationToken = default(CancellationToken)
    )
    {
      var append = body != null && !bodyAsJson && method == HttpMethod.Get ? ((requestUri.Contains("?") ? "&" : "?") + (body as Dictionary<string, string>).ToQueryString()) : "";
      var request = new HttpRequestMessage(method, requestUri + append.Replace("%252F","/"));

      // content of the request
      if (body != null && !bodyAsJson && method != HttpMethod.Get)
      {
        request.Content = new FormUrlEncodedContent(body as Dictionary<string, string>);
      }
      else if (body != null && method != HttpMethod.Get)
      {
        request.Content = new StringContent(JsonConvert.SerializeObject(body));
      }

      // OAuth header
      if (isOauth)
      {
        request.Headers.Add("Authorization", $"OAuth {AccessToken}");
      }

      
      return await Request(request, cancellationToken).ConfigureAwait(false);
    }


    public async Task<string> Request(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
    {
      HttpResponseMessage response = null;
      string responseString;

      // make async request
      try
      {
        response = await SendAsync(request, cancellationToken).ConfigureAwait(false);

        // validate HTTP response
        ValidateResponse(response);

        // read response
        responseString = await response.Content.ReadAsStringAsync();
      }
      catch (HttpRequestException exc)
      {
        throw new FeedlySharpException(exc.Message, exc);
      }
      catch (FeedlySharpException exc)
      {
        throw exc;
      }
      finally
      {
        request.Dispose();

          response?.Dispose();
      }

      return responseString;
    }


      /// <summary>
      /// Converts JSON to Pocket objects
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="json">Raw JSON response</param>
      /// <returns></returns>
      /// <exception>Parse error.
      ///     <cref>PocketException</cref>
      /// </exception>
      private T DeserializeJson<T>(string json) where T : class, new()
    {
      json = json.Replace("[]", "{}");

      // deserialize object
      var parsedResponse = JsonConvert.DeserializeObject<T>(
        json,
        new JsonSerializerSettings
        {
          Error = (sender, args) =>
          {
            throw new FeedlySharpException(string.Format("Parse error: {0}", args.ErrorContext.Error.Message));
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new TimeSpanConverter(),
            new NullableIntConverter(),
            new UriConverter()
          }
        }
      );

      return parsedResponse;
    }


      /// <summary>
      /// Validates the response.
      /// </summary>
      /// <param name="response">The response.</param>
      /// <returns></returns>
      /// <exception>
      /// Error retrieving response
      ///     <cref>PocketException</cref>
      /// </exception>
      private void ValidateResponse(HttpResponseMessage response)
    {
      // no error found
      if (response.IsSuccessStatusCode)
      {
        return;
      }

      throw new Exception(response.StatusCode.ToString()); // TODO
    }


    /// <summary>
    /// Tries to fetch a header value.
    /// </summary>
    /// <param name="headers">The headers.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    private string TryGetHeaderValue(HttpResponseHeaders headers, string key)
    {
      string result = null;

      if (headers == null || string.IsNullOrEmpty(key))
      {
        return null;
      }

      foreach (var header in headers)
      {
        if (header.Key == key)
        {
          var headerEnumerator = header.Value.GetEnumerator();
          headerEnumerator.MoveNext();

          result = headerEnumerator.Current;
          break;
        }
      }

      return result;
    }
  }
}
