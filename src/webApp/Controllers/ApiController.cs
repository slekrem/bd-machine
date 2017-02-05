namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using dal.Implementations;
	using dal.Interfaces;

	public class ApiController : Controller
    {
		private readonly IContext _context;

		public ApiController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public ApiController() : this(new Context("name=MySql")) { }
		
		public JsonResult RawHostsThisYearInMonths() 
		{
			var months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var year = DateTime.UtcNow.Year;
			return Json(months
			            .Select(month => _context
			                    .RawHosts.Where(rawHost => 
			                                    rawHost.Timestamp.Month == month && 
			                                    rawHost.Timestamp.Year == year)
			                    .ToList()
			                    .Count())
			            .ToList(), JsonRequestBehavior.AllowGet);
		}

		public JsonResult RawUrlsThisYearInMonths() 
		{
			var months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			var year = DateTime.UtcNow.Year;
			return Json(months
						.Select(month => _context
								.RawUrls
								.Where(rawUrl =>
									   rawUrl.Timestamp.Month == month &&
									   rawUrl.Timestamp.Year == year)
								.ToList()
								.Count())
			            .ToList(), JsonRequestBehavior.AllowGet);
		}

		public JsonResult UrlGraph(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id));
			
			var rawHtml = _context
				.RawHtmls
				.OrderByDescending(x => x.Id)
				.FirstOrDefault(x => x.RawUrlId == id);

			var nodes = _context
				.CrawledUrls
				.Where(x => x.RawHtmlId == rawHtml.Id)
				.OrderBy(x => x.RawUrlId)
				.Select(x => x.RawUrlId)
				.Distinct()
				.ToList()
				.Select(x => _context.RawUrls.Find(x))
				.Select(x => new UrlGraphNode()
				{
					id = x.Id,
					label = x.Data
				})
				.ToList();
			

			/*
			nodes.Add(new UrlGraphNode() 
			{
				id = id,
				label = "asd"
			});
			*/;
			return Json(new UrlGraphJsonModel()
			{
				Nodes = nodes,
				Edges = _context
					.CrawledUrls
					.Where(x => x.RawHtmlId == rawHtml.Id)
					.Select(x => new UrlGraphEdge()
					{
						from = id,
						to = x.RawUrlId
					})
					.Distinct()
					.ToList(),
			}, JsonRequestBehavior.AllowGet);
		}
    }

	public class UrlGraphJsonModel 
	{
		public IEnumerable<UrlGraphNode> Nodes { get; set; }

		public IEnumerable<UrlGraphEdge> Edges { get; set; }
	}

	public class UrlGraphNode 
	{
		public int id { get; set; }

		public string label { get; set; }
	}

	public class UrlGraphEdge 
	{
		public int from { get; set; }

		public int to { get; set; }
	}

	public static class AsdMagic
	{
		public static Uri ToUriOrDefault(this string uriString) 
		{
			if (string.IsNullOrWhiteSpace(uriString))
				throw new ArgumentNullException(nameof(uriString));
			Uri uri = null;
			Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri);
			return uri;
		}
	}
}
