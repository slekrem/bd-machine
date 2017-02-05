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
							var uri = crawlableUrl.ToUri();
							var rawHtmlEntity = uri
								.GetHtmlAsByteArrayFromUri()
								.CreateRawHtmlFromByteArray(context, crawlableUrl.RawUrlId)
								.Log(x => string.Format("CreateRawHtmlFromByteArray: {0}", x.RawUrl.Data));
							/*
							var crawledUrls = rawHtmlEntity
								.Data
								.ToHtmlString()
								.ToHtmlDocument()
								.GetAbsoluteUrls(uri.Host);

							crawledUrls
								.Select(x => context.CreateCrawledHost(x, rawHtmlEntity.Id)
										.Log(y => string.Format("CreateCrawledHost: {0}", y.RawHost.Data)))
								.ToList();
							crawledUrls
								.Select(x => context.CreateCrawledUrl(x, rawHtmlEntity.Id)
										.Log(y => string.Format("CreateCrawledUrl: {0}", y.RawUrl.Data)))
								.ToList();
							crawledUrls
								.Select(x => context.GetOrCreateCrawlableUrl(x)
										.Log(y => string.Format("GetOrCreateCrawlableUrl: {0}", y.RawUrl.Data)))
								.ToList();
							*/
						} 
						catch (Exception e) 
						{
							Console.WriteLine(e.Message);
						}
						crawlableUrl.IsActivated = false;
						context.UpdateCrawlableUrl(crawlableUrl);
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			_crawlerIsBusy = false;
		}
	}
}
