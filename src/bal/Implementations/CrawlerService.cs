namespace bd.machine.bal.Implementations
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using dal.Implementations.Entities;
	using dal.Interfaces;
	using Interfaces;
	using Models;

	/*
	public class CrawlerService : ICrawlerService
	{
		public int AddCrawlableUrl(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (string.IsNullOrWhiteSpace(uri.Host))
				throw new ArgumentNullException("uri.Host");
			var rawUrlId = GetOrCreateUrlIdFromUri(uri);
			var crawlableUrl = _unitOfWork
				.CrawlableUrlRepository
				.CrawlableUrls
				.SingleOrDefault(x => x.RawUrlId == rawUrlId);
			if (crawlableUrl != null)
				return crawlableUrl.Id;
			crawlableUrl = new CrawlableUrlEntity()
			{
				IsActivated = true,
				RawUrlId = rawUrlId,
				Timestamp = DateTime.UtcNow
			};
			_unitOfWork
				.CrawlableUrlRepository
				.Create(crawlableUrl);
			return crawlableUrl.Id;
		}

		public IEnumerable<CrawlableUrlServiceModel> GetCrawlableUrls()
		{
			return _unitOfWork
				.CrawlableUrlRepository
				.CrawlableUrls.ToList()
				.Select(x => new CrawlableUrlServiceModel() 
			{
				Id = x.Id,
				IsActivated = x.IsActivated,
				RawUrl = x.RawUrl.Data,
				RawUrlId = x.RawUrlId
			});
		}

		private int GetOrCreateUrlIdFromUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (string.IsNullOrWhiteSpace(uri.Host))
				throw new ArgumentNullException("uri.Host");
			var rawUrlEntity = _unitOfWork
				.RawUrlRepository
				.RawUrls
				.SingleOrDefault(x => x.Data.ToLower() == uri.OriginalString.ToLower());
			if (rawUrlEntity != null)
				return rawUrlEntity.Id;
			rawUrlEntity = new RawUrlEntity()
			{
				Data = uri.OriginalString,
				Timestamp = DateTime.UtcNow,
				RawHostId = GetOrCreateHostIdFromUri(uri)
			};
			_unitOfWork
				.RawUrlRepository
				.Create(rawUrlEntity);
			return rawUrlEntity.Id;
		}

		private int GetOrCreateHostIdFromUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (string.IsNullOrWhiteSpace(uri.Host))
				throw new ArgumentNullException("uri.Host");
			var rawHostEntity = _unitOfWork
				.RawHostRepository
				.RawHosts
				.SingleOrDefault(x => x.Data.ToLower() == uri.Host.ToLower());
			if (rawHostEntity != null)
				return rawHostEntity.Id;
			rawHostEntity = new RawHostEntity()
			{
				Data = uri.Host,
				Timestamp = DateTime.UtcNow
			};
			_unitOfWork
				.RawHostRepository
				.Create(rawHostEntity);
			return rawHostEntity.Id;
		}
	}
	*/

	public static class CrawlerMagic 
	{
		public static HttpClient CreateHttpClient()
		{
			var httpClient = new HttpClient(new HttpClientHandler()
			{
				Proxy = new WebProxy("torproxy", 8118),
				UseProxy = true,
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			});
			httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
			httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
			httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
			httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
			httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
			httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
			return httpClient;
		}
		
		public static byte[] GetHtmlAsByteArrayFromUri(this Uri uri) 
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
			byte[] sourceCode;
			using (var httpClient = CreateHttpClient())
			{
				var asd = httpClient.GetByteArrayAsync(uri.OriginalString);
				asd.Wait();
				sourceCode = asd.Result;
			}
			return sourceCode;
		}

		public static T Log<T>(this T type, Func<T, string> func) where T : class 
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (func == null)
				throw new ArgumentNullException("func");
			Console.WriteLine(func(type));
			return type;
		}
	}
}
