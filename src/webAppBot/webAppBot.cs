namespace bd.machine.webAppBot
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;

	public class webAppBot
	{

		static Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		static void Main(string[] args)
		{
			var rnd = new Random();
			while (true) 
			{
				try 
				{
					HttpClient h = new HttpClient(new HttpClientHandler()
					{
						AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
						Proxy = new WebProxy("torproxy", 8118),
						UseProxy = true
					});

					h.DefaultRequestHeaders.Add("Connection", "keep-alive");
					h.DefaultRequestHeaders.Add("Pragma", "no-cache");
					h.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
					h.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
					h.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
					h.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
					h.DefaultRequestHeaders.Add("Referer", "http://asd01.localtunnel.me/go/magic?Term=https%3A%2F%2Fthehiddenwiki.org%2F");
					h.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, sdch");
					h.DefaultRequestHeaders.Add("Accept-Language", "de,en-US;q=0.8,en;q=0.6");
					Task<HttpResponseMessage> response = h.GetAsync("http://coinurl.com/get.php?id=61178");
					response.Wait(3000);
					var responseText = response.Result.Content.ReadAsStringAsync();
					responseText.Wait();
					System.Console.WriteLine(DateTime.UtcNow); // you would know what to do with the data ;)
				} 
				catch (Exception e) 
				{
					Console.WriteLine(e.Message);
				}
				Thread.Sleep((int)new TimeSpan(0, rnd.Next(8, 22), 0).TotalMilliseconds);
			}
		}


























		private static bool _crawlerIsBusy;

		public static void Main2(string[] args)
		{
			Console.WriteLine("Press \'q\' to quit the sample.");
			using (var timer = new Timer(StartCrawling, null, 0, 6000))
				while (Console.Read() != 'q') { }
			Console.WriteLine("END MAIN");
		}

		private static void StartCrawling(object state)
		{
			if (_crawlerIsBusy)
				return;
			_crawlerIsBusy = true;
			try
			{
				Console.WriteLine("start webAppBot: " + DateTime.UtcNow);
				using (var httpClient = new HttpClient())
				{
					httpClient.BaseAddress = new Uri("https://asd01.localtunnel.me/");
					httpClient.DefaultRequestHeaders.Clear();
					httpClient.DefaultRequestHeaders.Host = "coinurl.com";
					//httpClient.DefaultRequestHeaders.Connection = "";
					//httpClient.DefaultRequestHeaders.CacheControl = new CacheControl();
					//httpClient.DefaultRequestHeaders.Upgrade
					//httpClient.DefaultRequestHeaders.UserAgent.Add

					var src = httpClient.GetStringAsync("http://127.0.0.1:8080/");
					src.Wait();
					Console.WriteLine(src.Result);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			_crawlerIsBusy = false;
			Console.WriteLine("end bot");
		}
	}
}
