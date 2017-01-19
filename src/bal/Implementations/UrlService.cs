namespace bd.machine.bal.Implementations
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using dal.Implementations.Models;
	using dal.Interfaces;
	using Interfaces;

	public class UrlService : IUrlService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UrlService(IUnitOfWork unitOfWork) 
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			_unitOfWork = unitOfWork;
		}
		
		public IDictionary<int, Uri> GetAllUris()
		{
			return _unitOfWork
				.UrlRepository
				.Urls
				.ToList()
				.ToDictionary(x => x.Id, x => GetUri(x));
		}

		public int GetCrawledSitesCountByUrlId(int urlId)
		{
			if (urlId <= 0)
				throw new ArgumentOutOfRangeException("urlId");

			return _unitOfWork
				.UrlRawHtmlRepository
				.UrlRawHtml
				.Where(x => x.UrlId == urlId)
				.Count();
		}

		private Uri GetUri(Url url)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			var uriString = url.Scheme.Value;
			uriString += "://";
			uriString += url.Host.Value;
			uriString += url.Path.Value;
			if (url.Query != null)
				uriString += url.Query.Value;
			if (url.Fragment != null)
				uriString += url.Fragment.Value;

			return new Uri(uriString);
		}
	}
}
