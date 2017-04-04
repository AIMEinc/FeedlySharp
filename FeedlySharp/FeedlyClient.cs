using System;
using System.Net;

namespace FeedlySharp
{
  public partial class FeedlyClient : IDisposable, IFeedlyClient
  {
    /// <summary>
    /// The environment of feedly.
    /// </summary>
    public readonly CloudEnvironment Environment;

    private readonly string _clientId;

    private readonly string _clientSecret;

    private readonly string _redirectUri;

    private string CloudUri => GetCloudUri(Environment);

      private string AccessToken { get; set; }

    private string UserId { get; set; }

    private FeedlyHttpClient Client { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedlyClient"/> class.
    /// </summary>
    /// <param name="environment">The environment of feedly.</param>
    /// <param name="clientId">The client id.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <exception cref="System.ArgumentNullException">redirectUri;Authentication and token generation requires an URI to redirect to afterwards</exception>
    public FeedlyClient(CloudEnvironment environment, string clientId, string clientSecret, string redirectUri)
    {
      if (string.IsNullOrEmpty(redirectUri))
      {
        throw new ArgumentNullException(nameof(redirectUri), "Authentication and token generation requires an URI to redirect to afterwards");
      }

      Environment = environment;
      _clientId = clientId;
      _clientSecret = clientSecret;
      _redirectUri = redirectUri;
      Client = new FeedlyHttpClient(new Uri(CloudUri, UriKind.Absolute));
    }

    /// <summary>
    /// Activates the client to call methods which need authentication.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="userId">The user id.</param>
    public void Activate(string accessToken, string userId)
    {
      AccessToken = accessToken;
      UserId = userId;
      Client.AccessToken = accessToken;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Client.Dispose();
    }


    private string GetCloudUri(CloudEnvironment environment)
    {
      return $"https://{(environment == CloudEnvironment.Production ? "cloud" : "sandbox")}.feedly.com";
    }

    private string ValueToResource(string key, string value, bool encode = true)
    {
      string text;
      if (key == "feed") text = value.StartsWith("feed/") ? value : "feed/" + value;
      else text = value.StartsWith("user/") ? value : $"user/{UserId}/{key}/{value}";

      return encode ? WebUtility.UrlEncode(text) : text;
    }

    private string ValueToResource(ContentType type, string value, bool encode = true)
    {
      var key = type == ContentType.Feed ? "feed" : (type == ContentType.Tag ? "tag" : (type == ContentType.SystemCategory ? "systemcategory" : "category"));
      if (key == "systemcategory")
      {
        return encode ? WebUtility.UrlEncode(value) : value;
      }

      return ValueToResource(key, value, encode);
    }
  }

  public enum CloudEnvironment
  {
    Production,
    Sandbox
  }

  public enum ContentType
  {
    Feed,
    Category,
    SystemCategory,
    Tag
  }
}
