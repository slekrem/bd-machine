namespace bd.machine.hostCrawler
{
	using System;
	using System.Linq;
	using System.Threading;
	using bal.Implementations;
	using dal.Implementations;

	class HostCrawler
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
				Console.WriteLine("start crawling hosts: " + DateTime.UtcNow);
				using (var context = new Context("name=MySql")) 
				{
					context
						.RawHtmls
						.Where(rawHtml => rawHtml.CrawledHosts == false)
						.ToList()
						.ForEach(rawHtml =>
					{
						try 
						{
							Console.WriteLine("Try start handle hosts: " + rawHtml.RawUrl.Data);
							rawHtml
								.Data
								.ToHtmlString()
								.ToHtmlDocument()
								.GetAbsoluteUrls(rawHtml.RawUrl.Data.ToUri(UriKind.Absolute).Host)
								.ToList()
								.Select(x => context.CreateCrawledHost(x, rawHtml.Id)
								        .Log(y => string.Format("CreateCrawledHost: {0}", y.RawHost.Data)))
								.ToList();
							rawHtml.CrawledHosts = true;
							context.UpdateRawHtml(rawHtml);
						} 
						catch (Exception e)
						{
							Console.WriteLine("fail handle hosts: " + e.Message);
						}
					});
				}
				Console.WriteLine("finished! " + DateTime.UtcNow);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("fail handle hosts: " + e.Message);
			}
			_crawlerIsBusy = false;
			Console.WriteLine("crawler is done!");
		}
	}
}
