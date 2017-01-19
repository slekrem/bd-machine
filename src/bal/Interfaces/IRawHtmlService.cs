namespace bd.machine.bal.Interfaces
{
	using Implementations.Models;

	public interface IRawHtmlService
	{
		void SaveRawHtmlAsByteArray(byte[] rawHtml, int urlId);

		RawHtmlServiceModel GetRawHtmlRawHtmlServiceModelById(int rawHtmlId);
	}
}
