namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Interfaces;
	using bal.Implementations;
	using dal.Implementations;
	using dal.Interfaces;
	using ViewModels.Home;
	using System.Collections.Generic;

	public class HomeController : Controller
	{
		private readonly IUrlService _urlService;
		
		public HomeController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_urlService = new UrlService(new UnitOfWork(context));
		}

		public HomeController() : this(new Context("name=MySql")) { }
		
		public ActionResult Index()
		{
			var urls = new List<HomeIndexUrlViewModel>();
			var uris = _urlService.GetAllUris();
			foreach (var uri in uris)
				urls.Add(new HomeIndexUrlViewModel()
			{
				Id = uri.Key,
				Url = uri.Value.ToString(),
				SitesCount = _urlService.GetCrawledSitesCountByUrlId(uri.Key),
				LastRequest = _urlService.GetLastRequestDateTimeByUrlId(uri.Key).ToString()
			});
			
			return View(new HomeIndexViewModel() 
			{
				Urls = urls
			});
		}
	}
}
