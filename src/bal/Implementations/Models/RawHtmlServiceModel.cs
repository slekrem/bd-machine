namespace bd.machine.bal.Implementations.Models
{
	using System;

	public class RawHtmlServiceModel
	{
		public int RawHtmlId { get; set; }

		public int UrlId { get; set; }

		public string RawHtml { get; set; }

		public DateTime DownloadDateTime { get; set; }
	}
}
