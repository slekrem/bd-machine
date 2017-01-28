namespace bd.machine.webApp.ViewModels.Url
{
	using System.Collections.Generic;
	
	public class UrlHtmlsViewModel
	{
		public int Id { get; set; }

		public string Url { get; set; }
		
		public IEnumerable<HtmlModel> Htmls { get; set; }
	}
}
