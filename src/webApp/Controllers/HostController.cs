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
    }
}
