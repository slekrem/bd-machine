namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using ViewModels.Html;
	using dal.Implementations;
	using dal.Interfaces;
	using bd.machine.bal.Implementations;
	using System.Collections.Generic;
	using System.Linq;

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
				Text = new HtmlTextViewModel() 
				{
					TextLines = _context
						.RawHtmls
						.Find(id)
						.Data
						.ToHtmlDocument()
						.GetTextLines()
				}
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
				Urls = new HtmlUrlsViewModel() 
				{
					Urls = GetUrls(id)
				}
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
						.ToHtmlString()
				}
			});
		}

		public ActionResult Keywords(int id) 
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			return View("Id", new HtmlIdViewModel()
			{
				Id = id,
				Keywords = new HtmlKeywordsViewModel()
				{
					Keywords = _context
						.RawHtmls
						.Find(id)
						.Data
						.GetKeywordsFromRawHtmlData()
						.OrderByDescending(x => x.Value)
						.ToDictionary(x => x.Key, y => y.Value)
				}
			});
		}

		private IEnumerable<string> GetUrls(int rawHtmlId) 
		{
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			return _context
						.RawHtmls
						.Find(rawHtmlId)
						.Data
						.ToHtmlDocument()
						.GetUrls(_context
								 .RawHosts
								 .Find(_context
									   .RawUrls
									   .Find(_context
											 .RawHtmls
											 .Find(rawHtmlId)
											 .RawUrlId)
									   .RawHostId)
								 .Data);
		}
    }
}
