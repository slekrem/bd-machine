namespace bd.machine.bal.Implementations
{
	using System;
	using dal.Implementations.Models;
	using dal.Interfaces;
	using Interfaces;

	public class RawHtmlService : IRawHtmlService
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public RawHtmlService(IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");
			_unitOfWork = unitOfWork;
		}

		public void SaveRawHtmlAsByteArray(byte[] rawHtml, int urlId)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			if (urlId <= 0)
				throw new ArgumentOutOfRangeException("urlId");

			var rawHtmlEntry = new RawHtml();
			rawHtmlEntry.Value = rawHtml;
			_unitOfWork.RawHtmlRepository.Create(rawHtmlEntry);
			_unitOfWork.UrlRawHtmlRepository.Create(new UrlRawHtml() 
			{
				UrlId = urlId,
				RawHtmlId = rawHtmlEntry.Id,
				CreationDate = DateTime.Now
			});
		}
	}
}
