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
		private readonly IRawHtmlService _rawHtmlService;
		private readonly IUrlService _urlService;

		public RawHtmlController(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			var unitOfWork = new UnitOfWork(context);
			_rawHtmlService = new RawHtmlService(unitOfWork);
			_urlService = new UrlService(unitOfWork);
		}

		public RawHtmlController() : this(new Context("name=MySql")) { }

		public ActionResult Details(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException("id");
			var rawHtmlServiceModel = _rawHtmlService.GetRawHtmlRawHtmlServiceModelById(id);

			return View(new RawHtmlDetailsViewModel()
			{
				Url = _urlService.GetUriByUrlId(rawHtmlServiceModel.UrlId).ToString(),
				RawHtml = rawHtmlServiceModel.RawHtml,
				DownloadDateTime = rawHtmlServiceModel.DownloadDateTime.ToString(),
				PageTitle = _rawHtmlService.GetHtmlTitleFromRawHtml(rawHtmlServiceModel.RawHtml),
				HtmlMetaTags = _rawHtmlService.GetHtmlMetaTagsFromRawHtml(rawHtmlServiceModel.RawHtml)
			});
		}
	}
}
