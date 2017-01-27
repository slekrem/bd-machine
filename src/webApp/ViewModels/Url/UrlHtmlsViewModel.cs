namespace bd.machine.webApp.ViewModels.Url
{
	using System.Collections.Generic;
	
	public class UrlHtmlsViewModel
	{
		public IEnumerable<HtmlModel> Htmls { get; set; }
	}

	public class HtmlModel 
	{
		public int Id { get; set; }

		public string Timestamp { get; set; }

		public int DataLength { get; set; }
	}
}
