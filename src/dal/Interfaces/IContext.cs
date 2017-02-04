namespace bd.machine.dal.Interfaces
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using Implementations.Entities;

	public interface IContext : IDisposable
	{
		DbSet<RawHtmlEntity> RawHtmls { get; set; }

		DbSet<RawHostEntity> RawHosts { get; set; }

		DbSet<RawUrlEntity> RawUrls { get; set; }

		DbSet<CrawlableUrlEntity> CrawlableUrls { get; set; }

		DbSet<CrawledHostEntity> CrawledHosts { get; set; }

		DbSet<CrawledUrlEntity> CrawledUrls { get; set; }

		int SaveChanges();

		DbEntityEntry Entry(object entity);
	}
}
