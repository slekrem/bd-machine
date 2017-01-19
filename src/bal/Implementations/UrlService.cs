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

		public int CreateNewUrl(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var url = new Url() 
			{
				SchemeId = GetSchemeIdByUri(uri),
				HostId = GetHostIdByUri(uri),
				PortId = GetPortIdByUri(uri),
				PathId = GetPathIdByUri(uri),
				QueryId = GetQueryIdByUri(this, uri),
				FragmentId = GetFragmentIdByUri(uri),
				UrlMetadataId = GetUrlMetadataId()
			};
			_unitOfWork.UrlRepository.Create(url);
			return url.Id;
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

		public DateTime GetLastRequestDateTimeByUrlId(int urlId)
		{
			if (urlId <= 0)
				throw new ArgumentNullException("urlId");
			var lastUrlRawHtmlEntry = _unitOfWork
				.UrlRawHtmlRepository
				.UrlRawHtml
				.Where(x => x.UrlId == urlId)
				.ToList()
				.LastOrDefault();
			if (lastUrlRawHtmlEntry == null)
				return DateTime.MinValue;
			return lastUrlRawHtmlEntry.CreationDate;
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

		private int GetSchemeIdByUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var scheme = _unitOfWork
				.SchemeRepository
				.Schemes
				.SingleOrDefault(x => x.Value.ToLower() == uri.Scheme.ToLower());
			if (scheme == null) 
			{
				scheme = new Scheme()
				{
					Value = uri.Scheme
				};
				_unitOfWork.SchemeRepository.Create(scheme);
			}
			return scheme.Id;
		}

		private int GetHostIdByUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var host = _unitOfWork
				.HostRepository
				.Hosts
				.SingleOrDefault(x => x.Value.ToLower() == uri.Host.ToLower());
			if (host == null) 
			{
				host = new Host()
				{
					Value = uri.Host
				};
				_unitOfWork.HostRepository.Create(host);
			}
			return host.Id;
		}

		private int GetPortIdByUri(Uri uri) 
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var port = _unitOfWork
				.PortRepository
				.Ports
				.SingleOrDefault(x => x.Value == uri.Port);
			if (port == null)
			{
				port = new Port()
				{
					Value = uri.Port
				};
				_unitOfWork.PortRepository.Create(port);
			}
			return port.Id;
		}

		private int GetPathIdByUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var pathAsSting = uri.PathAndQuery;
			if (pathAsSting.Contains('?'))
				pathAsSting = pathAsSting.Split('?').First();
			var path = _unitOfWork
				.PathRepository
				.Paths
				.SingleOrDefault(x => x.Value.ToLower() == pathAsSting.ToLower());
			if (path == null)
			{
				path = new Path() 
				{
					Value = pathAsSting
				};
				_unitOfWork.PathRepository.Create(path);
			}
			return path.Id;
		}

		private Nullable<int> GetQueryIdByUri(UrlService instance, Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var queryAsSting = uri.PathAndQuery;
			if (queryAsSting.Contains('?'))
				queryAsSting = queryAsSting.Split('?').Last();
			var query = _unitOfWork
				.QueryRepository
				.Queries
				.SingleOrDefault(x => x.Value.ToLower() == queryAsSting.ToLower());
			if (query == null)
			{
				query = new Query()
				{
					Value = queryAsSting
				};
				_unitOfWork.QueryRepository.Create(query);
			}
			return query.Id;
		}

		Nullable<int> GetFragmentIdByUri(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			var fragment = _unitOfWork
				.FragmentRepository
				.Fragments
				.SingleOrDefault(x => x.Value.ToLower() == uri.Fragment.ToLower());
			if (fragment == null)
			{
				fragment = new Fragment()
				{
					Value = uri.Fragment
				};
				_unitOfWork.FragmentRepository.Create(fragment);
			}
			return fragment.Id;
		}

		private int GetUrlMetadataId() 
		{
			var urlMetadata = _unitOfWork
				.UrlMetadataRepository
				.UrlMetadatas
				.FirstOrDefault();
			if (urlMetadata == null)
				throw new NotImplementedException();
			return urlMetadata.Id;
		}
	}
}
