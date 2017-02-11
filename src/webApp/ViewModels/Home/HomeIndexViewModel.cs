namespace bd.machine.webApp.ViewModels.Home
{
	using System.Collections.Generic;

	public class HomeIndexViewModel
	{
		public string Term { get; set; }

		public IEnumerable<SearchResult> SearchResults { get; set; }

		public HomeIndexResponseViewModel Response { get; set; }
		
		public IEnumerable<HomeIndexHostViewModel> Hosts { get; set; }
	}

	public class SearchResult 
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string Url { get; set; }
	}
}
