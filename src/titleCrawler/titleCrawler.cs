namespace bd.machine.titleCrawler
{
	using System;
	using System.Linq;
	using System.Threading;
	using bal.Implementations;
	using dal.Implementations;
	using dal.Implementations.Entities;

	class titleCrawler
	{
		private static bool _crawlerIsBusy;

		public static void Main(string[] args)
		{
			Console.WriteLine("Press \'q\' to quit the sample.");
			using (var timer = new Timer(StartCrawling, null, 0, 600000))
				while (Console.Read() != 'q') { }
		}

		private static void StartCrawling(object state)
		{
			if (_crawlerIsBusy)
				return;
			_crawlerIsBusy = true;
			try
			{
				Console.WriteLine("start crawling titles: " + DateTime.UtcNow);
				using (var context = new Context("name=MySql"))
				{
					context
						.RawHtmls
						.Where(rawHtml => rawHtml.CrawledTitle == false)
						.ToList()
						.ForEach(rawHtml =>
					{
						try
						{
							Console.WriteLine("Try start handle html: " + rawHtml.RawUrl.Data);
							var title = rawHtml
								.Data
								.ToHtmlString()
								.ToHtmlDocument()
								.GetHtmlTitleOrDefault();
							rawHtml.CrawledTitle = true;
							context.UpdateRawHtml(rawHtml);
							if (title == null)
								return;
							var rawTitle = context.GetOrCreateRawTitle(rawHtml.RawUrlId, title);
							context.CreateCrawledTitle(new CrawledTitleEntity() 
							{
								RawHtmlId = rawHtml.Id,
								RawTitleId = rawTitle.Id,
								UtcTimestamp = DateTime.UtcNow
							});
							Console.WriteLine("created successful title: " + rawTitle.Data);
						}
						catch (Exception e)
						{
							Console.WriteLine("fail handle urls: " + e.Message);
						}
					});
				}
				Console.WriteLine("finished! " + DateTime.UtcNow);
			}
			catch (Exception e)
			{
				Console.WriteLine("fail crawling urls: " + e.Message);
			}
			_crawlerIsBusy = false;
			Console.WriteLine("crawler is done!");
		}
	}
}
