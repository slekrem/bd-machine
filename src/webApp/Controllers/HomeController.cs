namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
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

		public ActionResult Index()
		{
			return View(new HomeIndexViewModel()
			{
				Hosts = _context
					.RawHosts
					.ToList()
					.Select(x => new HomeIndexHostViewModel()
					{
						Id = x.Id,
						Host = x.Data,
						Urls = _context
							.RawUrls
							.Where(rawUrl => rawUrl.RawHostId == x.Id).ToList()
							.Count,
						Sites = _context
							.RawHtmls.Count()
					})
			});
		}
	}
}
