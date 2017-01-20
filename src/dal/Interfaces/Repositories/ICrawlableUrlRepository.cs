namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Entities;

	public interface ICrawlableUrlRepository
	{
		IQueryable<CrawlableUrlEntity> CrawlableUrls { get; }

		void Create(CrawlableUrlEntity crawlableUrlEntity);
	}
}
