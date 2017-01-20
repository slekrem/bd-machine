namespace bd.machine.bal.Interfaces
{
	using System.Collections.Generic;
	using Models.Htmltags;
	using Implementations.Models;

	public interface IRawHtmlService
	{
		void SaveRawHtmlAsByteArray(byte[] rawHtml, int urlId);

		RawHtmlServiceModel GetRawHtmlRawHtmlServiceModelById(int rawHtmlId);

		string GetHtmlTitleFromRawHtml(string rawHtml);

		IEnumerable<HtmlMetaTag> GetHtmlMetaTagsFromRawHtml(string rawHtml);

		IEnumerable<string> GetHtmlTextByRawHtmlId(int rawHtmlId);

		IEnumerable<string> GetUrlsFromRawHtmlById(int rawHtmlId);
	}
}
