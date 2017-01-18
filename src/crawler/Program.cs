namespace db.machine.crawler
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using bd.machine.bal.Implementations;
	using bd.machine.dal.Implementations;

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
				var urlService = new UrlService(unitOfWork);
				var rawHtmlService = new RawHtmlService(unitOfWork);

				foreach (var uri in urlService.GetAllUris())
				{
					var rawHtmlTask = GetRawHtmlAsync(uri.Value);
					rawHtmlTask.Wait();
					rawHtmlService.SaveRawHtmlAsByteArray(rawHtmlTask.Result, uri.Key);
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
