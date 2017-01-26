namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Linq;
	using System.Text;
	using System.Web.Mvc;
	using bal.Implementations;
	using bal.Interfaces;
	using dal.Implementations;
	using dal.Interfaces;
	using PagedList;
	using ViewModels.Url;

	public class UrlController : Controller
	{
		private readonly IContext _context;
		private readonly IUrlService _urlService;
		
		public UrlController(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
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

		[HttpGet]
		public ActionResult Data(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View(new UrlDataViewModel()
			{
				RawUrlId = id,
				RawUrl = _context
					.RawUrls
					.Single(x => x.Id == id)
					.Data,
				RawHosts = _context
					.RawHtmls
					.Where(x => x.RawUrlId == id)
					.ToList()
					.Select(x => new UrlDataRawHtmlViewModel() 
				{
					Id = x.Id,
					Timestamp = x.Timestamp,
					Preview = ToPreview(x.Data)
				})
			});
		}

		private string ToPreview(byte[] rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			var html = Encoding.Default.GetString(rawHtml);
			if (html.Length <= 16)
				return html;
			return html.Substring(0, 16);
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
					TrafficInKilobyte = x.TrafficInKilobyte + " kB",
					RawHtmlId = x.RawHtmlId
				})
				.ToPagedList(page, 10);
		}
	}
}
