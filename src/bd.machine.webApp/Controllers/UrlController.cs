namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using bal.Implementations;
	using bal.Interfaces;
	using dal.Implementations;
	using dal.Interfaces;
	using PagedList;
	using ViewModels.Url;

	public class UrlController : Controller
	{
		private readonly IUrlService _urlService;

		public UrlController(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_urlService = new UrlService(new UnitOfWork(context));
		}

		public UrlController() : this(new Context("name=MySql")) { }

		[HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(UrlAddViewModel model)
		{
			if (model == null)
				throw new ArgumentNullException("model");
			if (!ModelState.IsValid)
				return View(model);
			Uri uri = null;
			if (!Uri.TryCreate(model.Url, UriKind.Absolute, out uri))
				return View(model);
			return RedirectToAction("Details", "Url", new
			{
				id = _urlService.CreateNewUrl(uri)
			});
		}

		[HttpGet]
		public ActionResult Details(int id, Nullable<int> page)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			if (!page.HasValue)
				page = 1;
			return View(new UrlDetailsViewModel()
			{
				Url = _urlService.GetUriByUrlId(id).ToString(),
				RequestPagedList = GetUrlRequestsByUrlId(id, page.Value),
			});
		}

		private IPagedList<UrlRequestViewModel> GetUrlRequestsByUrlId(int urlId, int page)
		{
			if (urlId <= 0)
				throw new ArgumentOutOfRangeException("urlId");
			return _urlService
				.GetUrlRequestsByUrlId(urlId)
				.OrderByDescending(x => x.RequestDateTime)
				.Select(x => new UrlRequestViewModel()
				{
					Id = x.Id,
					LastRequest = x.RequestDateTime.ToString(),
					TrafficInKilobyte = x.TrafficInKilobyte.ToString() + " kB"
				})
				.ToPagedList(page, 10);
		}
	}
}
