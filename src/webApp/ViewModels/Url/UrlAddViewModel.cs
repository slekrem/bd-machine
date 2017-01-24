namespace bd.machine.webApp.ViewModels.Url
{
	using System.ComponentModel.DataAnnotations;

	public class UrlAddViewModel
	{
		[Required]
		[Display(Name = "URL")]
		public string Url { get; set; }
	}
}
