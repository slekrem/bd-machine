namespace bd.machine.descriptionCrawler
{
	using System;
	using System.Linq;
	using System.Threading;
	using bal.Implementations;
	using dal.Implementations.Entities;
	using dal.Implementations;

	class descriptionCrawler
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
				Console.WriteLine("start crawling description: " + DateTime.UtcNow);
				using (var context = new Context("name=MySql"))
				{
					context
						.RawHtmls
						.Where(rawHtml => rawHtml.CrawledDescription == false)
						.ToList()
						.ForEach(rawHtml =>
					{
						try
						{
							rawHtml.CrawledDescription = true;
							context.UpdateRawHtml(rawHtml);
							Console.WriteLine("Try start handle html: " + rawHtml.RawUrl.Data);
							var description = rawHtml
								.Data
								.ToHtmlString()
								.ToHtmlDocument()
								.GetHtmlDescriptionOrDefault();
							if (description == null)
								return;
							var rawDescription = context.CreateRawDescription(new RawDescriptionEntity()
							{
								Data = description,
								RawUrlId = rawHtml.RawUrlId,
								UtcTimestamp = DateTime.UtcNow
							});
							var crawledDescription = context.CreateCrawledDescription(new CrawledDescriptionEntity()
							{
								RawDescriptionId = rawDescription.Id,
								RawHtmlId = rawHtml.Id,
								UtcTimestamp = DateTime.UtcNow
							});
							Console.WriteLine("created successful description: " + rawDescription.Data);
						}
						catch (Exception e)
						{
							Console.WriteLine("fail handle description: " + e.Message);
						}
					});
				}
				Console.WriteLine("finished! " + DateTime.UtcNow);
			}
			catch (Exception e)
			{
				Console.WriteLine("fail crawling description: " + e.Message);
			}
			_crawlerIsBusy = false;
			Console.WriteLine("crawler is done!");
		}
	}
}
