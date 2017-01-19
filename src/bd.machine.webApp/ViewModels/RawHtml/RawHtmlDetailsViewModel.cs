namespace bd.machine.webApp.ViewModels.RawHtml
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using bd.machine.bal.Models.Htmltags;

	public class RawHtmlDetailsViewModel
	{
		public string Url { get; set; }

		public string DownloadDateTime { get; set; }

		public string RawHtml { get; set; }

		[Display(Name = "Titel")]
		public string PageTitle { get; set; }

		public IEnumerable<HtmlMetaTag> HtmlMetaTags { get; set; }
	}
}
