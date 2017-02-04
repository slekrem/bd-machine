namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using bd.machine.dal.Implementations;
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
    }
}
