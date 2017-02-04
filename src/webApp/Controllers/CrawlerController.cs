namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Implementations;
	using bal.Interfaces;
	using dal.Implementations;
	using dal.Interfaces;
	using ViewModels.Crawler;

	public class CrawlerController : Controller
	{
		public CrawlerController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
		}

		public CrawlerController() : this(new Context("name=MySql")) { }
		
		[HttpGet]
		public ActionResult Index()
		{
			return View(new CrawlerIndexViewModel() 
			{
				//CrawlableUrls = _crawlerService.GetCrawlableUrls()
			});
		}

		[HttpGet]
		public ActionResult AddUrl() 
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddUrl(CrawlerAddUrlViewModel viewModel)
		{
			throw new NotImplementedException();
			/*

			if (viewModel == null)
				throw new ArgumentNullException("viewModel");
			if (!ModelState.IsValid)
				return View(viewModel);
			Uri uri = null;
			if (!Uri.TryCreate(viewModel.Url, UriKind.Absolute, out uri))
				return View(viewModel);
			return RedirectToAction("CrawlableUrl", new { id = _crawlerService.AddCrawlableUrl(uri) });
			*/
		}

		[HttpGet]
		public ActionResult CrawlableUrl(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			
			return View();
		}
	}
}
