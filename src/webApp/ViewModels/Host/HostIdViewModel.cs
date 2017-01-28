namespace bd.machine.webApp.ViewModels.Host
{
	using System.Collections.Generic;

	public class HostIdViewModel
	{
		public int Id { get; set; }

		public string Host { get; set; }

		public IEnumerable<UrlModel> Urls { get; set; }
	}
}
