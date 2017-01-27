namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using ViewModels.Html;
	using dal.Implementations;
	using dal.Interfaces;
	using bd.machine.bal.Implementations;

	public class HtmlController : Controller
    {
		private readonly IContext _context;

		public HtmlController(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public HtmlController() : this(new Context("name=MySql")) { }
		
        public ActionResult Id(int id)
        {
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View (new HtmlIdViewModel() 
			{
				Id = id
			});
        }

		public ActionResult Text(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HtmlIdViewModel() 
			{
				Id = id,
				Text = new HtmlTextViewModel() { }
			});
		}

		public ActionResult Images(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HtmlIdViewModel()
			{
				Id = id,
				Images = new HtmlImagesViewModel() { }
			});
		}

		public ActionResult Urls(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HtmlIdViewModel()
			{
				Id = id,
				Urls = new HtmlUrlsViewModel() { }
			});
		}

		public ActionResult Html(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HtmlIdViewModel()
			{
				Id = id,
				Html = new HtmlHtmlViewModel() 
				{
					Raw = _context
						.RawHtmls
						.Find(id)
						.Data
						.ToHtml()
				}
			});
		}
    }
}
