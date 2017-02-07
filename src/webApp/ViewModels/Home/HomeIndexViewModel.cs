namespace bd.machine.webApp.ViewModels.Home
{
	using System.Collections.Generic;

	public class HomeIndexViewModel
	{
		public string Term { get; set; }

		public HomeIndexResponseViewModel Response { get; set; }
		
		public IEnumerable<HomeIndexHostViewModel> Hosts { get; set; }
	}
}
