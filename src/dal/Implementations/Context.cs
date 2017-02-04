namespace bd.machine.dal.Implementations
{
	using System.Data.Entity;
	using Interfaces;
	using Entities;
	using System;

	public class Context : DbContext, IContext
	{
		public DbSet<RawHtmlEntity> RawHtmls { get; set; }

		public DbSet<RawHostEntity> RawHosts { get; set; }

		public DbSet<RawUrlEntity> RawUrls { get; set; }

		public DbSet<CrawlableUrlEntity> CrawlableUrls { get; set; }

		public DbSet<CrawledHostEntity> CrawledHosts { get; set; }

		public DbSet<CrawledUrlEntity> CrawledUrls { get; set; }

		public Context(string connectionString)
			: base(connectionString) { }
	}
}
