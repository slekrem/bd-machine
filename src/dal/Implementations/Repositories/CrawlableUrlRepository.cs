namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Entities;
	using Interfaces;
	using Interfaces.Repositories;

	public class CrawlableUrlRepository : ICrawlableUrlRepository
	{
		private readonly IContext _context;
		
		public IQueryable<CrawlableUrlEntity> CrawlableUrls
		{
			get
			{
				return _context.CrawlableUrls;
			}
		}
		
		public CrawlableUrlRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(CrawlableUrlEntity crawlableUrlEntity)
		{
			if (crawlableUrlEntity == null)
				throw new ArgumentNullException("crawlableUrlEntity");
			_context.CrawlableUrls.Add(crawlableUrlEntity);
			_context.SaveChanges();
		}
	}
}
