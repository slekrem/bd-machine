namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Interfaces;
	using bal.Implementations;
	using dal.Implementations;
	using dal.Interfaces;
	using ViewModels.Home;
	using System.Collections.Generic;

	public class HomeController : Controller
	{
		public HomeController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
		}

		public HomeController() : this(new Context("name=MySql")) { }
		
		public ActionResult Index()
		{
			return View(new HomeIndexViewModel() 
			{
			});
		}
	}
}
