namespace bd.machine.dal.Implementations
{
	using System;
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
	}
}
