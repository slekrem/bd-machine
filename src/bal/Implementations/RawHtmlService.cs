namespace bd.machine.bal.Implementations
{
	using System;
	using dal.Interfaces;
	using Interfaces;
	using System.Linq;
	using System.Text;
	using HtmlAgilityPack;
	using bal.Models.Htmltags;
	using System.Collections.Generic;
	using dal.Implementations.Entities;
	using bd.machine.dal.Implementations;

	public class RawHtmlService : IRawHtmlService
	{
		public IEnumerable<HtmlMetaTag> GetHtmlMetaTagsFromRawHtml(string rawHtml)
		{
			if (string.IsNullOrWhiteSpace(rawHtml))
				throw new ArgumentNullException("rawHtml");
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(rawHtml);
			var metaNodes = htmlDocument.DocumentNode.SelectNodes("//meta");
			if (metaNodes.Count == 0)
				return new List<HtmlMetaTag>();
			return metaNodes
				.Select(x => new HtmlMetaTag()
				{
					Name = x.GetAttributeValue("name", ""),
					Content = x.GetAttributeValue("content", "")
				});
		}

		public IEnumerable<string> GetHtmlTextByRawHtmlId(int rawHtmlId)
		{
			throw new ArgumentNullException();
			
			/*
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			var rawHtmlEntry = _unitOfWork
				.RawHtmlRepository
				.RawHtmls.Single(x => x.Id == rawHtmlId);
			var rawHtml = Encoding.Default.GetString(rawHtmlEntry.Data);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(rawHtml);

			var asd = new List<string>();
			foreach (var node in htmlDocument.DocumentNode.SelectNodes("//text()"))
			{
				if (!string.IsNullOrWhiteSpace(node.InnerText))
				{
					var parentNodeName = node.ParentNode.Name.ToLower();
					if (parentNodeName != "script" &&
						parentNodeName != "a" &&
						parentNodeName != "title" &&
						parentNodeName != "header")
					{
						asd.Add(node.InnerText);
					}
				}
			}

			return asd;
			*/
		}

		public IEnumerable<string> GetUrlsFromRawHtmlById(int rawHtmlId)
		{
			throw new ArgumentNullException();
			/*
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			var rawHtmlEntry = _unitOfWork
				.RawHtmlRepository
				.RawHtmls
				.Single(x => x.Id == rawHtmlId);
			var rawHostEntity = _unitOfWork
				.RawHostRepository
				.RawHosts
				.Single(x => x.Id == rawHtmlEntry.RawUrl.RawHostId);

			var rawHtml = Encoding.Default.GetString(rawHtmlEntry.Data);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(rawHtml);
			var asd = new List<string>();
			foreach (var node in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
			{
				// Get the value of the HREF attribute
				string hrefValue = node.GetAttributeValue("href", string.Empty);

				Uri uri = null;
				if (Uri.TryCreate(hrefValue, UriKind.Absolute, out uri))
				{
					if (!string.IsNullOrWhiteSpace(uri.Host))
						asd.Add(uri.OriginalString);
					else {
						var uriBuilder = new UriBuilder(uri);
						uriBuilder.Scheme = "http://";
						uriBuilder.Host = rawHostEntity.Data;
						asd.Add(uriBuilder.Uri.ToString());
					}
				}
			}
			return asd;
			*/
		}

		public string GetHtmlTitleFromRawHtml(string rawHtml)
		{
			if (string.IsNullOrWhiteSpace(rawHtml))
				throw new ArgumentNullException("rawHtml");
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(rawHtml);
			var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
			if (titleNode == null)
				return string.Empty;
			return titleNode.InnerHtml;
		}

		public void SaveRawHtmlAsByteArray(byte[] rawHtml, int rawUrlId)
		{
			throw new NotImplementedException();
			/*
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			if (rawUrlId <= 0)
				throw new ArgumentOutOfRangeException("rawUrlId");
			_unitOfWork
				.RawHtmlRepository
				.Create(new RawHtmlEntity()
				{
					RawUrlId = rawUrlId,
					Data = rawHtml,
					Timestamp = DateTime.UtcNow
				});
			*/
		}
	}

	public static class Asd
	{
		public static string ToHtmlString(this byte[] rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			return Encoding.Default.GetString(rawHtml);
		}

		public static HtmlDocument ToHtmlDocument(this byte[] rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(Encoding.Default.GetString(rawHtml));
			return htmlDocument;
		}

		public static HtmlDocument ToHtmlDocument(this string rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(rawHtml);
			return htmlDocument;
		}

		public static IEnumerable<string> GetTextLines(this HtmlDocument htmlDocument)
		{
			if (htmlDocument == null)
				throw new ArgumentNullException("htmlDocument");
			var textLines = new List<string>();
			htmlDocument
				.DocumentNode
				.SelectNodes("//text()")
				.ToList()
				.ForEach(x => { if (x.HasText()) textLines.Add(x.InnerText); });
			return textLines;
		}

		public static string GetTextFromHtmlSourceCode(this string htmlSourceCode)
		{
			if (string.IsNullOrWhiteSpace(htmlSourceCode))
				throw new ArgumentNullException("htmlSourceCode");
			var text = string.Empty;
			htmlSourceCode
				.ToHtmlDocument()
				.DocumentNode
				.SelectNodes("//text()")
				.ToList()
				.ForEach(htmlNode => { if (htmlNode.HasText()) { text += htmlNode.InnerText; } });
			return text;
		}

		public static bool HasText(this HtmlNode htmlNode)
		{
			if (htmlNode == null)
				return false;
			if (string.IsNullOrWhiteSpace(htmlNode.InnerText))
				return false;
			var blackList = new[] { "script", "a", "title", "header" };
			var parentNodeName = htmlNode.ParentNode.Name.ToLower();
			if (blackList.Contains(parentNodeName))
				return false;
			return true;
		}

		public static IEnumerable<Uri> GetAbsoluteUrls(this HtmlDocument htmlDocument, string originHost) 
		{
			if (htmlDocument == null)
				throw new ArgumentNullException("htmlDocument");
			if (string.IsNullOrWhiteSpace(originHost))
				throw new ArgumentNullException("originHost");
			try 
			{
				var absoluteUrls = new List<Uri>();
				htmlDocument
					.DocumentNode
					.SelectNodes("//a[@href]")
					.ToList()
					.ForEach(node =>
				{
					string hrefValue = node.GetAttributeValue("href", string.Empty);
					Uri url = null;
					if (Uri.TryCreate(hrefValue, UriKind.Absolute, out url))
					{
						if (!string.IsNullOrWhiteSpace(url.Host))
							absoluteUrls.Add(url);
						else
						{
							var uriBuilder = new UriBuilder(url);
							uriBuilder.Scheme = "http://";
							uriBuilder.Host = originHost;
							absoluteUrls.Add(uriBuilder.Uri);
						}
					}
				});
				return absoluteUrls;
			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.Message);
				return new List<Uri>();
			}
		}

		public static IEnumerable<string> GetUrls(this HtmlDocument htmlDocument, string originHost)
		{
			if (htmlDocument == null)
				throw new ArgumentNullException("htmlDocument");
			if (string.IsNullOrWhiteSpace(originHost))
				throw new ArgumentNullException("originHost");

			var asd = new List<string>();
			foreach (var node in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
			{
				// Get the value of the HREF attribute
				string hrefValue = node.GetAttributeValue("href", string.Empty);

				Uri uri = null;
				if (Uri.TryCreate(hrefValue, UriKind.Absolute, out uri))
				{
					if (!string.IsNullOrWhiteSpace(uri.Host))
						asd.Add(uri.OriginalString);
					else {
						var uriBuilder = new UriBuilder(uri);
						uriBuilder.Scheme = "http://";
						uriBuilder.Host = originHost;
						asd.Add(uriBuilder.Uri.ToString());
					}
				}
			}
			return asd;
		}

		public static IDictionary<string, int> GetKeywordsFromRawHtmlData(this byte[] rawHtmlData)
		{
			if (rawHtmlData == null)
				throw new ArgumentNullException("rawHtmlData");
			return rawHtmlData
				.ToHtmlString()
				.GetTextFromHtmlSourceCode()
				.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
				.WordsCount();
		}

		public static IDictionary<string, int> WordsCount(this IEnumerable<string> words)
		{
			if (words == null)
				throw new ArgumentNullException("words");
			var dictionary = new Dictionary<string, int>();
			words.ToList().ForEach(word =>
			{
				if (dictionary.ContainsKey(word))
					++dictionary[word];
				else
					dictionary.Add(word, 1);
			});
			return dictionary;
		}

		public static Uri ToUri(this string uriString, UriKind uriKind) 
		{
			if (string.IsNullOrWhiteSpace(uriString)) return null;
			Uri uri = null;
			Uri.TryCreate(uriString, uriKind, out uri);
			return uri;
		}

		public static Uri ToAbsoluteUri(this string uriString) 
		{
			if (string.IsNullOrWhiteSpace(uriString))
				throw new ArgumentNullException("uriString");
			Uri uri = null;
			if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri))
				throw new Exception();
			return uri;
		}

		public static Uri ToAbsoluteUriOrDefault(this string uriString)
		{
			if (string.IsNullOrWhiteSpace(uriString))
				return null;
			Uri uri = null;
			if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri))
				return null;
			return uri;
		}

		public static IEnumerable<Uri> CreateCrawledHosts(this IEnumerable<Uri> urls, IContext context, int rawHtmlId)
		{
			if (urls == null)
				throw new ArgumentNullException("urls");
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			urls.ToList().ForEach(url => 
			{
				var rawHostEntity = url.GetOrCreateRawHostEntity(context);
				context.CreateCrawledHostEntity(rawHostEntity.Id, rawHtmlId);
			});
			return urls;
		}

		public static IEnumerable<Uri> CreateCrawledUrls(this IEnumerable<Uri> urls, IContext context, int rawHtmlId) 
		{
			if (urls == null)
				throw new ArgumentNullException("urls");
			if (context == null)
				throw new ArgumentNullException("context");
			if (rawHtmlId <= 0)
				throw new ArgumentOutOfRangeException("rawHtmlId");
			urls.ToList().ForEach(url => 
			{
				var rawUrlEntity = context.GetOrCreateRawUrlEntity(url);
				context.CreateCrawledUrlEntity(rawUrlEntity.Id, rawHtmlId);
			});
			return urls;
		}
	}
}
