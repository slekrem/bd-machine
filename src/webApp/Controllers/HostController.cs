namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using ViewModels.Host;
	using dal.Implementations;
	using dal.Interfaces;
	using System.Linq;

	public class HostController : Controller
    {
		private readonly IContext _context;

		public HostController(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public HostController() : this(new Context("name=MySql")) { }
		
        public ActionResult Index()
        {
			return View(new HostIndexViewModel()
			{
				Hosts = _context
					.RawHosts
					.ToList()
					.Select(rawHost => new HostModel()
					{
						Id = rawHost.Id,
						Host = rawHost.Data,
						Urls = _context
						.RawUrls
						.Where(rawUrl => rawUrl.RawHostId == rawHost.Id)
						.ToList()
						.Count
					})
			});
        }

		public ActionResult Id(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HostIdViewModel 
			{
				Id = id,
				Host = _context
					.RawHosts
					.Find(id)
					.Data
			});
		}

		public ActionResult Urls(int id)
		{
			if (id <= 0)
				throw new ArgumentNullException("id");
			return View(new HostUrlsViewModel()
			{
				Urls = _context
					.RawUrls
					.Where(rawUrl => rawUrl.RawHostId == id)
					.ToList()
					.Select(rawUrl => new UrlModel()
					{
						Id = rawUrl.Id,
						Url = rawUrl.Data,
						Htmls = _context
						.RawHtmls
						.Where(rawHtml => rawHtml.RawUrlId == rawUrl.Id)
						.ToList()
						.Count
					})
			});
		}

		public ActionResult Data(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			var months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			return Json(
				months.Select(month => _context.RawUrls.Where(x => x.RawHostId == id && x.Timestamp.Month == month)
				              .ToList().Count),
				JsonRequestBehavior.AllowGet);
		}
    }
}
