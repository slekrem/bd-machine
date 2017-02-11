namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using bal.Implementations;
	using dal.Implementations.Entities;
	using dal.Implementations;
	using dal.Interfaces;
	using ViewModels.Home;

	public class HomeController : Controller
	{
		private readonly IContext _context;
		
		public HomeController(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public HomeController() : this(new Context("name=MySql")) { }

		[HttpGet]
		public ActionResult Index()
		{
			return View(new HomeIndexViewModel());
		}

		[HttpGet]
		public ActionResult Magic(string term) 
		{
			if (string.IsNullOrWhiteSpace(term))
				return View("Index", new HomeIndexViewModel());

			var urls = _context
				.RawTitles
				.Where(x => x.Data.Contains(term))
				.Select(x => x.RawUrlId)
				.ToList()
				.Select(x => _context.RawUrls.Find(x).Data)
				.ToList();

			return View("Index", new HomeIndexViewModel()
			{
				SearchResults = _context
					.RawTitles
					.Where(x => x.Data.Contains(term))
					.Select(x => x.RawUrlId)
					.ToList()
					.Select(rawUrlId => new SearchResult()
					{
						Title = _context
							.RawTitles
							.OrderByDescending(x => x.RawUrlId == rawUrlId)
							.FirstOrDefault()
							.Data,
						Url = _context
							.RawUrls.Find(rawUrlId).Data
					})
			});
		}








		[HttpGet]
		public ActionResult MagicA(HomeIndexViewModel model) 
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (!ModelState.IsValid)
				return View("Index", model);
			var uri = model.Term.ToUriOrDefault();
			if (uri == null)
				return View("Index", model);
			if (!uri.IsAbsoluteUri)
				return View("Index", model);
			var rawUrlEntity = _context.GetOrCreateRawUrlEntity(uri);
			var crawlableUrls = _context.GetOrCreateCrawlableUrl(uri);
			crawlableUrls.IsActivated = true;
			_context.UpdateCrawlableUrl(crawlableUrls);
			var rawHtmlEntity = _context.GetLastRawHtmlOrDefault(x => x.RawUrlId == rawUrlEntity.Id);
			if (rawHtmlEntity == null)
				return View("Index", model);
			model.Response = new HomeIndexResponseViewModel()
			{
				UrlId = rawUrlEntity.Id,
				Urls = _context
					.CrawledUrls
					.Where(x => x.RawHtmlId == rawHtmlEntity.Id)
					.Select(x => x.RawUrlId)
					.Distinct()
					.ToList()
					.Select(x => _context.RawUrls.Find(x).Data)
					.ToList()
			};
			return View("Index", model);
		}


		[HttpGet]
		public new ActionResult Url(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			var rawUrlEntity = _context.RawUrls.Find(id);
			if (rawUrlEntity == null)
				throw new NullReferenceException("rawUrlEntity");
			return View("Url", new HomeUrlViewModel() 
			{
				Id = id,
				Url = rawUrlEntity.Data
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public new ActionResult Url(HomeIndexViewModel model) 
		{
			if (model == null)
				throw new ArgumentNullException("model");
			var uri = model.Term.ToAbsoluteUriOrDefault();
			if (uri == null)
			{
				ModelState.AddModelError(string.Empty, "url is empty");
				return View("Index", model);
			}
			var rawUrlEntity = _context.RawUrls
			                           .FirstOrDefault(x => x.Data.ToLower() == uri.OriginalString.ToLower());
			if (rawUrlEntity != null) 
			{
				_context.ActivateCrawlableUrl(rawUrlEntity.Id);
				return RedirectToAction("Url", new { id = rawUrlEntity.Id });
			}
			var rawHostEntity = _context
				.RawHosts
				.FirstOrDefault(x => x.Data.ToLower() == uri.Host.ToLower());
			if (rawHostEntity == null)
			{
				rawHostEntity = new RawHostEntity()
				{
					Data = uri.Host,
					Timestamp = DateTime.UtcNow
				};
				_context
					.RawHosts
					.Add(rawHostEntity);
				_context.SaveChanges();
			}
			rawUrlEntity = new RawUrlEntity()
			{
				Data = uri.OriginalString,
				RawHostId = rawHostEntity.Id,
				Timestamp = DateTime.UtcNow
			};
			_context
				.RawUrls
				.Add(rawUrlEntity);
			_context.SaveChanges();
			var crawlableUrlEntity = new CrawlableUrlEntity()
			{
				IsActivated = true,
				RawUrlId = rawUrlEntity.Id,
				Timestamp = DateTime.UtcNow
			};
			_context
				.CrawlableUrls
				.Add(crawlableUrlEntity);
			_context.SaveChanges();
			return RedirectToAction("Url", new { id = rawUrlEntity.Id });
		}
	}
}
