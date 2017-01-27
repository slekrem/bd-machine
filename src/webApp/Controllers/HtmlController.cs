namespace bd.machine.webApp.Controllers
{
	using System;
	using System.Web.Mvc;
	using ViewModels.Html;
	using dal.Implementations;
	using dal.Interfaces;

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
			var html = _context.RawHtmls.Find(id);
			return View (new HtmlIdViewModel() 
			{
				
			});
        }
    }
}
