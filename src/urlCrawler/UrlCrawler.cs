namespace bd.machine.urlCrawler
{
	using System;
	using System.Linq;
	using System.Threading;
	using bd.machine.bal.Implementations;
	using dal.Implementations;

	public class MainClass
	{
		private static bool _crawlerIsBusy;

		public static void Main(string[] args)
		{
			using (var timer = new Timer(StartCrawling, null, 0, 600000))
			{
				Console.WriteLine("Press \'q\' to quit the sample.");
				while (Console.Read() != 'q') { }
			}
		}

		private static void StartCrawling(object state)
		{
			if (_crawlerIsBusy)
				return;
			_crawlerIsBusy = true;
			try
			{
				using (var context = new Context("name=MySql"))
				{
					context
						.RawHtmls
						.Where(rawHtml => rawHtml.CrawledUrls == false)
						.ToList()
						.ForEach(rawHtml =>
					{
						try
						{
							rawHtml
								.Data
								.ToHtmlString()
								.ToHtmlDocument()
								.GetAbsoluteUrls(rawHtml.RawUrl.Data.ToUri(UriKind.Absolute).Host)
								.ToList()
								.Select(x => context.CreateCrawledUrl(x, rawHtml.Id)
								        .Log(y => string.Format("CreateCrawledUrl: {0}", y.RawUrl.Data)))
								.ToList();
							rawHtml.CrawledUrls = true;
							context.UpdateRawHtml(rawHtml);
						}
						catch (Exception e)
						{
							Console.WriteLine(e.Message);
						}
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
