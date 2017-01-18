namespace db.machine.crawler
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using bd.machine.bal.Implementations;
	using bd.machine.dal.Implementations;

	class Program
	{
		public static void Test() 
		{
			using (var ctx = new Context("name=MySql")) 
			{
				var asd = ctx.Urls.ToList();
				foreach (var a in asd) 
				{
					Console.WriteLine(a.Id);
				}
			}
		}
		
		public static void Main(string[] args)
		{
			//Test();
			//return;
			
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
				var sourceCodeService = new RawHtmlService(unitOfWork);

				foreach (var uri in urlService.GetAllUris())
				{
					var rawHtmlTask = GetRawHtmlAsync(uri.Value);
					rawHtmlTask.Wait();
					var sourceCode = rawHtmlTask.Result;

					sourceCodeService.SaveRawHtmlAsByteArray(sourceCode, uri.Key);

					Console.WriteLine(sourceCode);
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
			}) { })
				sourceCode = await httpClient.GetByteArrayAsync(uri);
			return sourceCode;
		}
}
}
