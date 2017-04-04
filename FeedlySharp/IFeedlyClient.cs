using System;
using System.Threading;
using FeedlySharp.Models;

namespace FeedlySharp
{
  public interface IFeedlyClient
  {
    void Activate(string accessToken, string userId);
    System.Threading.Tasks.Task<bool> AddOrUpdateSubscription(string id, System.Collections.Generic.List<FeedlyCategory> categories = null, string optionalTitle = null, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> AddOrUpdateTopic(string topicId, Interest interest = Interest.Low, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task DeleteCategory(string id, CancellationToken cancellationToken = default(CancellationToken));
    void Dispose();
    System.Threading.Tasks.Task<FeedlySearchResponse> FindEntries(string contentId, string searchQuery, DateTime? newerThan = null, string continuation = null, string fields = null, string embedded = null, string engagement = null, int? count = null, string locale = null, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySearchFeed>> FindFeeds(string searchQuery, int? count = null, string locale = null, CancellationToken cancellationToken = default(CancellationToken));
    string GetAuthenticationUri(string scope = "subscriptions", string state = default(string));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyCategory>> GetCategories(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyEntry>> GetEntries(string[] ids, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyEntry> GetEntry(string id, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyFeed> GetFeed(string id, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyFeed>> GetFeeds(string[] ids, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyReadOperations> GetMarkersReadOperations(DateTime newerThan, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyCategoryUnreadCount>> GetMarkersUnreadCount(bool isAutoRefresh = false, DateTime? newerThan = null, string categoryId = null, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyEntry>> GetMixes(string contentId, int? count = null, bool unreadOnly = false, int? limitHours = null, DateTime? newerThan = null, bool backfill = true, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<string> GetOpml(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetPreferences(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyUser> GetProfile(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyStreamEntriesResponse> GetStreamEntries(string id, ContentType type, int? count = null, FeedSorting sorting = FeedSorting.Newest, bool? unreadOnly = null, DateTime? newerThan = null, string continuation = null, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyStreamEntryIdsResponse> GetStreamEntryIds(string id, ContentType type, int? count = null, FeedSorting sorting = FeedSorting.Newest, bool? unreadOnly = null, DateTime? newerThan = null, string continuation = null, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlySubscription>> GetSubscriptions(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyTag>> GetTags(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.List<FeedlyTopic>> GetTopics(CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> ImportOpml(string opml, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> KeepEntriesAsUnread(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> KeepEntryAsUnread(string entryId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkCategoriesAsRead(string[] categoryIds, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkCategoryAsRead(string categoryId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntriesAsRead(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntriesAsSaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntriesAsUnsaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntryAsRead(string entryId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntryAsSaved(string entryId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkEntryAsUnsaved(string entryId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkFeedAsRead(string feedId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> MarkFeedsAsRead(string[] feedIds, CancellationToken cancellationToken = default(CancellationToken));
    AuthenticationResponse ParseAuthenticationResponseUri(string uri);
    AuthenticationResponse ParseAuthenticationResponseUri(Uri uri);
    System.Threading.Tasks.Task<bool> RemoveSubscription(string id, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTags(string[] tags, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RemoveTopic(string topicId, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task RenameCategory(string id, string label, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> RenameTag(string oldTag, string newTag, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<AccessTokenResponse> RequestAccessToken(string authenticationCode, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<AccessTokenResponse> RequestRefreshToken(string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> UpdatePreferences(System.Collections.Generic.Dictionary<string, string> preferences, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<FeedlyUser> UpdateProfile(System.Collections.Generic.Dictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> UpdateTags(string entryId, string[] tags, CancellationToken cancellationToken = default(CancellationToken));
    System.Threading.Tasks.Task<bool> UpdateTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken));
  }
}
