namespace bd.machine.bal.Interfaces
{
	using System;
	using System.Collections.Generic;
	using Implementations.Models;

	public interface ICrawlerService
	{
		int AddCrawlableUrl(Uri uri);

		IEnumerable<CrawlableUrlServiceModel> GetCrawlableUrls();
	}
}
