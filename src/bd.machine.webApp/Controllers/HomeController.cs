namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Interfaces;
	using bal.Implementations;
	using dal.Implementations;
	using dal.Interfaces;

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
			var mvcName = typeof(Controller).Assembly.GetName();
			var isMono = Type.GetType("Mono.Runtime") != null;

			ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
			ViewData["Runtime"] = isMono ? "Mono" : ".NET";
			ViewData["Uris"] = _urlService.GetAllUris()[2];

			return View();
		}
	}
}
