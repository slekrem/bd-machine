namespace bd.machine.webApp.ViewModels.Home
{
	using System.Collections.Generic;

	public class HomeIndexResponseViewModel
	{
		public int? UrlId { get; set; }

		public IEnumerable<string> Urls { get; set; }
	}
}
