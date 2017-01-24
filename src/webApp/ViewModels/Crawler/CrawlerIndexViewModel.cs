namespace bd.machine.webApp.ViewModels.Crawler
{
	using System.Collections.Generic;
	using bal.Implementations.Models;

	public class CrawlerIndexViewModel
	{
		public IEnumerable<CrawlableUrlServiceModel> CrawlableUrls { get; set; }
	}
}
