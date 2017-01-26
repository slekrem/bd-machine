namespace bd.machine.webApp.ViewModels.Url
{
	using System.Collections.Generic;

	public class UrlDataViewModel
	{
		public int RawUrlId { get; set; }
		
		public string RawUrl { get; set; }

		public IEnumerable<UrlDataRawHtmlViewModel> RawHosts { get; set; }
	}
}
