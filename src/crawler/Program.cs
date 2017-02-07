namespace bd.machine.crawler
{
	using System;
	using System.Linq;
	using System.Threading;
	using bal.Implementations;
	using dal.Implementations;

	class Program
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
				Console.WriteLine("Try start html crawling - " + DateTime.UtcNow);
				using (var context = new Context("name=MySql"))
				{
					context
						.CrawlableUrls
						.Where(x => x.IsActivated == true)
						.ToList()
						.ForEach(crawlableUrl =>
					{
						try 
						{
							Console.WriteLine("Try start handle html: " + crawlableUrl.RawUrl.Data);
							crawlableUrl.ToUri()
							            .GetHtmlAsByteArrayFromUri()
							            .CreateRawHtmlFromByteArray(context, crawlableUrl.RawUrlId)
							            .Log(x => string.Format("CreateRawHtmlFromByteArray: {0}", x.RawUrl.Data));
							crawlableUrl.IsActivated = false;
							context.UpdateCrawlableUrl(crawlableUrl);
						} 
						catch (Exception e) 
						{
							Console.WriteLine("fail handle html: " + e.Message);
						}
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("fail html crawling: " + e.Message);
			}
			_crawlerIsBusy = false;
		}
	}
}
