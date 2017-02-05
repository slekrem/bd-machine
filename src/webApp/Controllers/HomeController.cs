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
			return View();
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
			var uri = model.Url.ToAbsoluteUriOrDefault();
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
