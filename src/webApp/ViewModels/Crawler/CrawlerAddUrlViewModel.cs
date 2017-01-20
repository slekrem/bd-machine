namespace bd.machine.webApp.ViewModels.Crawler
{
	using System.ComponentModel.DataAnnotations;

	public class CrawlerAddUrlViewModel
	{
		[Required]
		[Display(Name = "Url")]
		public string Url { get; set; }
	}
}
