namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using bal.Implementations;
	using bal.Interfaces;
	using dal.Implementations;
	using dal.Interfaces;
	using ViewModels.RawHtml;

	public class RawHtmlController : Controller
	{
		private readonly IContext _context;

		public RawHtmlController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public RawHtmlController() : this(new Context("name=MySql")) { }

		public ActionResult Details(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View(new RawHtmlDetailsViewModel() { });
		}

		public ActionResult Data(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Index", new RawHtmlIndexViewModel() 
			{
				Id = id
			});
		}

		public ActionResult Text(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Index", new RawHtmlIndexViewModel() 
			{
				Id = id,
				Text = GetRawHtmlTextViewModel(id)
			});
		}

		public ActionResult Urls(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Index", new RawHtmlIndexViewModel()
			{
				Id = id,
				Urls = GetRawHtmlUrlsViewModel(id)
			});
		}

		private RawHtmlUrlsViewModel GetRawHtmlUrlsViewModel(int rawHtmlId)
		{
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			return new RawHtmlUrlsViewModel()
			{
				//Urls = _rawHtmlService.GetUrlsFromRawHtmlById(rawHtmlId)
			};
		}

		private RawHtmlTextViewModel GetRawHtmlTextViewModel(int rawHtmlId)
		{
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			return new RawHtmlTextViewModel()
			{
				//Text = _rawHtmlService.GetHtmlTextByRawHtmlId(rawHtmlId)
			};
		}
	}
}
