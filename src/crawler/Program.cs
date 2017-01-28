namespace bd.machine.crawler
{
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using bal.Implementations;
	using dal.Implementations;

	class Program
	{
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
			using (var context = new Context("name=MySql"))
			{
				var unitOfWork = new UnitOfWork(context);
				var crawlerService = new CrawlerService(unitOfWork);
				var rawHtmlService = new RawHtmlService(unitOfWork);
				foreach (var crawlableUrl in crawlerService
				         .GetCrawlableUrls()
				         .Where(x => x.IsActivated == true)) 
				{
					Uri uri = null;
					if (Uri.TryCreate(crawlableUrl.RawUrl, UriKind.Absolute, out uri))
					{
						var rawHtmlTask = GetRawHtmlAsync(uri);
						rawHtmlTask.Wait();
						var rawHtml = rawHtmlTask.Result;
						if (rawHtml.Length <= 0)
							Console.WriteLine("rawHtml size is too small: " + crawlableUrl.RawUrl);
						else 
						{
							rawHtmlService.SaveRawHtmlAsByteArray(rawHtmlTask.Result, crawlableUrl.RawUrlId);
							Console.WriteLine("crawling was successful: " + crawlableUrl.RawUrl);
						}
					}
					else 
					{
						Console.WriteLine("cannot create uri: " + crawlableUrl.RawUrl);
					}
				}
			}
		}

		private static async Task<byte[]> GetRawHtmlAsync(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
			byte[] sourceCode;

			using (var httpClient = new HttpClient(new HttpClientHandler()
			{
				Proxy = new WebProxy("torproxy", 8118),
				UseProxy = true
			})
			{ })
				sourceCode = await httpClient.GetByteArrayAsync(uri);
			return sourceCode;
		}
	}
}
