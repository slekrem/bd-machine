namespace bd.machine.webApp.ViewModels.RawHtml
{
	using System.ComponentModel.DataAnnotations;

	public class RawHtmlDetailsViewModel
	{
		public string Url { get; set; }

		public string DownloadDateTime { get; set; }

		public string RawHtml { get; set; }

		[Display(Name = "Titel")]
		public string PageTitle { get; set; }
	}
}
