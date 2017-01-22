namespace bd.machine.bal.Implementations
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using dal.Implementations.Entities;
	using dal.Interfaces;
	using Interfaces;
	using Models;

	public class CrawlerService : ICrawlerService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CrawlerService(IUnitOfWork unitOfWork) 
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");
			_unitOfWork = unitOfWork;
		}

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
				RawUrl = x.RawUrl.Data
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
}
