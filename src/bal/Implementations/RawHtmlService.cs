namespace bd.machine.bal.Implementations
{
	using System;
	using Models;
	using dal.Implementations.Models;
	using dal.Interfaces;
	using Interfaces;
	using System.Linq;
	using System.Text;
	using HtmlAgilityPack;
	using bal.Models.Htmltags;
	using System.Collections.Generic;
	using bd.machine.dal.Implementations.Entities;

	public class RawHtmlService : IRawHtmlService
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public RawHtmlService(IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");
			_unitOfWork = unitOfWork;
		}

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
		}

		public IEnumerable<string> GetUrlsFromRawHtmlById(int rawHtmlId)
		{
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
		}
	}

	public static class Asd 
	{
		public static string ToHtml(this byte[] rawHtml) 
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

		public static IEnumerable<string> GetTextLines(this HtmlDocument htmlDocument) 
		{
			if (htmlDocument == null)
				throw new ArgumentNullException("htmlDocument");
			var textLines = new List<string>();
			htmlDocument
				.DocumentNode
				.SelectNodes("//text()")
				.ToList()
				.ForEach(x => 
			{
				if (!string.IsNullOrWhiteSpace(x.InnerText)) {
					var parentNodeName = x.ParentNode.Name.ToLower();
					if (parentNodeName != "script" &&
						parentNodeName != "a" &&
						parentNodeName != "title" &&
						parentNodeName != "header") {
						textLines.Add(x.InnerText);
					}
				}
			});
			return textLines;
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
	}
}
