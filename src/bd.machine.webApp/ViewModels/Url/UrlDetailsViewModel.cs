namespace bd.machine.webApp.ViewModels.Url
{
	using PagedList;

	public class UrlDetailsViewModel
	{
		public string Url { get; set; }

		public IPagedList<UrlRequestViewModel> RequestPagedList { get; set; }
	}
}
