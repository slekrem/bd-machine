namespace bd.machine.bal.Implementations
{
	using System;
	using System.Linq;
	using bd.machine.dal.Implementations.Entities;
	using dal.Interfaces;
	using Interfaces;

	public class CrawlerService : ICrawlerService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CrawlerService(IUnitOfWork unitOfWork) 
		{
			if (unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");
			_unitOfWork = unitOfWork;
		}

		public int AddUrl(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
			if (string.IsNullOrWhiteSpace(uri.Host))
				throw new ArgumentNullException("uri.Host");
			var rawUrlEntity = _unitOfWork
				.RawUrlRepository
				.RawUrls
				.SingleOrDefault(x => x.Data.ToLower() == uri.OriginalString.ToLower());
			if (rawUrlEntity != null)
				return rawUrlEntity.Id;
			var rawHostEntity = _unitOfWork
				.RawHostRepository
				.RawHosts
				.SingleOrDefault(x => x.Data.ToLower() == uri.Host.ToLower());
			if (rawHostEntity == null)
			{
				rawHostEntity = new RawHostEntity()
				{
					Data = uri.Host,
					Timestamp = DateTime.UtcNow
				};
				_unitOfWork.RawHostRepository.Create(rawHostEntity);
			}
			rawUrlEntity = new RawUrlEntity()
			{
				RawHostId = rawHostEntity.Id,
				Timestamp = DateTime.UtcNow,
				Data = uri.OriginalString
			};
			_unitOfWork.RawUrlRepository.Create(rawUrlEntity);
			return rawUrlEntity.Id;
		}
	}
}
