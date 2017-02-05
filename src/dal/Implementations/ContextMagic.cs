namespace bd.machine.dal.Implementations
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using Entities;
	using Interfaces;

	public static class ContextMagic
	{
		public static T ConsoleWriteLineFor<T>(this T t, Func<T, string> action) where T : class
		{
			if (t == null)
				throw new ArgumentNullException("t");
			if (action == null)
				throw new ArgumentNullException("action");
			Console.WriteLine(action(t)); return t;
		}
		
		public static bool IsUriInDatabase(this IContext context, Uri uri) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (uri == null)
				throw new ArgumentNullException("uri");
			return context
				.RawUrls
				.SingleOrDefault(x => x.Data.ToLower() == uri.OriginalString.ToLower()) == null ? false : true;
		}

		public static bool IsHostInDatabase(this IContext context, string hostString)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (hostString == null)
				throw new ArgumentNullException("hostString");
			return context
				.RawHosts
				.SingleOrDefault(x => x.Data.ToLower() == hostString.ToLower()) == null ? false : true;
		}

		public static RawHostEntity CreateHost(this IContext context, string hostString)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (string.IsNullOrWhiteSpace(hostString))
				throw new ArgumentNullException("hostString");
			var hostEntity = new RawHostEntity() 
			{
				Data = hostString,
				Timestamp = DateTime.UtcNow
			};
			context.RawHosts.Add(hostEntity);
			context.SaveChanges();
			return hostEntity;
		}

		public static RawHostEntity GetRawHostByHostString(this IContext context, string hostString) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (string.IsNullOrWhiteSpace(hostString))
				throw new ArgumentNullException("hostString");
			return context
				.RawHosts
				.Single(x => x.Data.ToLower() == hostString.ToLower());
		}

		public static RawHostEntity GetOrCreateRawHostEntity(this Uri url, IContext context) 
		{
			if (url == null)
				throw new ArgumentNullException("url");
			if (context == null)
				throw new ArgumentNullException("context");
			if (string.IsNullOrWhiteSpace(url.Host))
				throw new ArgumentNullException("url.Host");
			var rawHostEntity = context
				.RawHosts
				.Where(x => x.Data.ToLower() == url.Host.ToLower())
				.FirstOrDefault();
			if (rawHostEntity != null)
				return rawHostEntity;
			rawHostEntity = new RawHostEntity() 
			{
				Data = url.Host,
				Timestamp = DateTime.UtcNow
			};
			context.RawHosts.Add(rawHostEntity);
			context.SaveChanges();
			return rawHostEntity;
		}

		public static CrawledHostEntity CreateCrawledHostEntity(this IContext context, int rawHostId, int rawHtmlId) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawHostId <= 0)
				throw new ArgumentOutOfRangeException("rawHostId");
			if (rawHtmlId <= 0)
				throw new ArgumentNullException("rawHtmlId");
			var crawledHostEntity = new CrawledHostEntity() 
			{
				RawHostId = rawHostId,
				RawHtmlId = rawHtmlId,
				UtcTimestamp = DateTime.UtcNow
			};
			context.CrawledHosts.Add(crawledHostEntity);
			context.SaveChanges();
			return crawledHostEntity;
		}

		public static CrawledUrlEntity CreateCrawledUrl(this IContext context, Uri uri, int rawHtmlId) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			var rawUrlEntity = context.GetOrCreateRawUrlEntity(uri);
			var crawledUrlEntity = new CrawledUrlEntity() 
			{
				RawUrlId = rawUrlEntity.Id,
				RawHtmlId = rawHtmlId,
				UtcTimestamp = DateTime.UtcNow
			};
			context.CrawledUrls.Add(crawledUrlEntity);
			context.SaveChanges();
			return crawledUrlEntity;
		}

		public static CrawledHostEntity CreateCrawledHost(this IContext context, Uri uri, int rawHtmlId) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			var rawHostEntity = context.GetOrCreateRawHostEntity(uri);
			var crawledHostEntity = new CrawledHostEntity()
			{
				RawHostId = rawHostEntity.Id,
				RawHtmlId = rawHtmlId,
				UtcTimestamp = DateTime.UtcNow
			};
			context.CrawledHosts.Add(crawledHostEntity);
			context.SaveChanges();
			return crawledHostEntity;
		}

		public static CrawlableUrlEntity GetCrawlableUrlOrDefault(this IContext context, Uri url)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (url == null)
				throw new ArgumentNullException("url");
			var rawUrlEntity = context
				.RawUrls
				.FirstOrDefault(x => x.Data.ToLower() == url.OriginalString.ToLower());
			if (rawUrlEntity == null)
				return null;
			return context
				.CrawlableUrls
				.FirstOrDefault(x => x.RawUrlId == rawUrlEntity.Id);
		}

		public static CrawledUrlEntity CreateCrawledUrlEntity(this IContext context, int rawUrlId, int rawHtmlId) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawUrlId <= 0)
				throw new ArgumentOutOfRangeException("rawUrlId");
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			var crawledUrlEntity = new CrawledUrlEntity()
			{
				RawUrlId = rawUrlId,
				RawHtmlId = rawHtmlId,
				UtcTimestamp = DateTime.UtcNow
			};
			context.CrawledUrls.Add(crawledUrlEntity);
			context.SaveChanges();
			return crawledUrlEntity;
		}

		public static RawUrlEntity CreateUri(this IContext context, Uri uri) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (!context.IsHostInDatabase(uri.Host))
				context.CreateHost(uri.Host);
			var rawHostEntity = context.GetRawHostByHostString(uri.Host);
			var rawUrlEntity = new RawUrlEntity()
			{
				Data = uri.OriginalString,
				RawHostId = rawHostEntity.Id,
				Timestamp = DateTime.UtcNow
			};
			context.RawUrls.Add(rawUrlEntity);
			context.SaveChanges();
			return rawUrlEntity;
		}

		public static RawHtmlEntity CreateRawHtmlFromByteArray(this byte[] htmlByteArray, IContext context, int rawUrlId) 
		{
			if (htmlByteArray == null)
				throw new ArgumentNullException("htmlByteArray");
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawUrlId <= 0)
				throw new ArgumentOutOfRangeException("rawUrlId");
			var rawHtmlEntity = new RawHtmlEntity() 
			{
				Data = htmlByteArray,
				RawUrlId = rawUrlId,
				Timestamp = DateTime.UtcNow
			};
			context.RawHtmls.Add(rawHtmlEntity);
			context.SaveChanges();
			return rawHtmlEntity;
		}

		public static RawUrlEntity GetRawUrlEntity(this Uri uri, IContext context) 
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (context == null)
				throw new ArgumentNullException("context");
			return context
				.RawUrls
				.Single(rawUrl => rawUrl.Data.ToLower() == uri.OriginalString.ToLower());
		}

		public static RawHostEntity GetOrCreateRawHostEntity(this IContext context, Uri url) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (url == null)
				throw new ArgumentNullException("url");
			var rawHostEntity = context
				.RawHosts
				.SingleOrDefault(x => x.Data.ToLower() == url.Host.ToLower());
			if (rawHostEntity != null)
				return rawHostEntity;
			rawHostEntity = new RawHostEntity() 
			{
				Data = url.Host,
				Timestamp = DateTime.UtcNow
			};
			context.RawHosts.Add(rawHostEntity);
			context.SaveChanges();
			return rawHostEntity;
		}

		public static RawUrlEntity GetOrCreateRawUrlEntity(this IContext context, Uri url) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (url == null)
				throw new ArgumentNullException("url");
			var rawUrlEntity = context
				.RawUrls
				.FirstOrDefault(x => x.Data.ToLower() == url.OriginalString.ToLower());
			if (rawUrlEntity != null)
				return rawUrlEntity;
			var rawHostEntity = context.GetOrCreateRawHostEntity(url);
			rawUrlEntity = new RawUrlEntity()
			{
				Data = url.OriginalString,
				RawHostId = rawHostEntity.Id,
				Timestamp = DateTime.UtcNow
			};
			context.RawUrls.Add(rawUrlEntity);
			context.SaveChanges();
			return rawUrlEntity;
		}

		public static RawUrlEntity GetRawUrlOrDefault(this IContext context, Uri url) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (url == null)
				throw new ArgumentNullException("url");
			return context.RawUrls.FirstOrDefault(x => x.Data.ToLower() == url.OriginalString.ToLower());
		}

		public static void ActivateCrawlableUrl(this IContext context, int rawUrlId) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawUrlId <= 0)
				throw new ArgumentOutOfRangeException("rawUrlId");
			var crawlableUrl = context
				.CrawlableUrls
				.SingleOrDefault(x => x.RawUrlId == rawUrlId);
			if (crawlableUrl == null)
			{
				crawlableUrl = new CrawlableUrlEntity()
				{
					RawUrlId = rawUrlId,
					IsActivated = true,
					Timestamp = DateTime.UtcNow
				};
				context.CrawlableUrls.Add(crawlableUrl);
				context.SaveChanges();
			}
			else 
			{
				crawlableUrl.IsActivated = true;
				context.Entry(crawlableUrl).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		public static CrawlableUrlEntity GetOrCreateCrawlableUrl(this IContext context, Uri url) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (url == null)
				throw new ArgumentNullException("url");
			var rawUrlEntity = context
				.GetOrCreateRawUrlEntity(url);
			var crawlableUrl = context
				.CrawlableUrls
				.FirstOrDefault(x => x.RawUrlId == rawUrlEntity.Id);
			if (crawlableUrl != null)
				return crawlableUrl;
			crawlableUrl = new CrawlableUrlEntity() 
			{
				IsActivated = true,
				RawUrlId = rawUrlEntity.Id,
				Timestamp = DateTime.UtcNow
			};
			context
				.CrawlableUrls
				.Add(crawlableUrl);
			context.SaveChanges();
			Console.WriteLine(string.Format("Create CrawlableUrl: {0}", crawlableUrl.RawUrl.Data)); 
			return crawlableUrl;
		}

		public static Uri ToUri(this CrawlableUrlEntity crawlableUrl) 
		{
			if (crawlableUrl == null)
				throw new ArgumentNullException("crawlableUrl");
			Uri uri = null;
			if (Uri.TryCreate(crawlableUrl.RawUrl.Data, UriKind.Absolute, out uri))
				return uri;
			throw new NullReferenceException("uri");
		}

		public static void UpdateCrawlableUrl(this IContext context, CrawlableUrlEntity crawlableUrl) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (crawlableUrl == null)
				throw new ArgumentNullException("crawlableUrl");
			context.Entry(crawlableUrl).State = EntityState.Modified;
			context.SaveChanges();
		}

		public static void UpdateRawHtml(this IContext context, RawHtmlEntity rawHtml) 
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (rawHtml == null)
				throw new ArgumentNullException(nameof(rawHtml));
			context.Entry(rawHtml).State = EntityState.Modified;
			context.SaveChanges();
		}
	}
}
