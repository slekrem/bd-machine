namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Implementations;
	using bal.Interfaces;
	using dal.Implementations;
	using dal.Interfaces;
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

		public ActionResult Details(int id) 
		{
			throw new NotImplementedException();
		}
	}
}
