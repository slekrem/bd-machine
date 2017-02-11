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

		public DbSet<RawTitleEntity> RawTitles { get; set; }

		public DbSet<RawDescriptionEntity> RawDescriptions { get; set; }

		public DbSet<CrawlableUrlEntity> CrawlableUrls { get; set; }

		public DbSet<CrawledHostEntity> CrawledHosts { get; set; }

		public DbSet<CrawledUrlEntity> CrawledUrls { get; set; }

		public DbSet<CrawledTitleEntity> CrawledTitles { get; set; }

		public DbSet<CrawledDescriptionEntity> CrawledDescriptions { get; set; }

		public Context(string connectionString)
			: base(connectionString) { }
	}
}
