namespace bd.machine.webApp.ViewModels.Url
{
	using System.Collections.Generic;

	public class UrlDetailsViewModel
	{
		public string Url { get; set; }

		public IEnumerable<UrlRequestViewModel> Requests { get; set; }
	}
}
