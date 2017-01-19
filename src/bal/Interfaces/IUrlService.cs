namespace bd.machine.bal.Interfaces
{
	using System;
	using System.Collections.Generic;
	using Implementations.Models;

	public interface IUrlService
	{
		IDictionary<int, Uri> GetAllUris();

		int GetCrawledSitesCountByUrlId(int urlId);

		int CreateNewUrl(Uri uri);

		DateTime GetLastRequestDateTimeByUrlId(int urlId);

		Uri GetUriByUrlId(int urlId);

		IEnumerable<UrlRequest> GetUrlRequestsByUrlId(int urlId);
	}
}
