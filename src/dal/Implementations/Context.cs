namespace bd.machine.dal.Implementations
{
	using System.Data.Entity;
	using Models;
	using Interfaces;
	using Entities;

	public class Context : DbContext, IContext
	{
		public DbSet<Fragment> Fragments { get; set; }

		public DbSet<Host> Hosts { get; set; }

		public DbSet<Path> Paths { get; set; }

		public DbSet<Port> Ports { get; set; }

		public DbSet<Query> Queries { get; set; }

		public DbSet<Scheme> Schemes { get; set; }

		public DbSet<UrlMetadata> UrlMetadatas { get; set; }

		public DbSet<Url> Urls { get; set; }

		public DbSet<RawHtmlEntity> RawHtmls { get; set; }

		public DbSet<UrlRawHtml> UrlRawHtml { get; set; }

		public DbSet<RawHostEntity> RawHosts { get; set; }

		public DbSet<RawUrlEntity> RawUrls { get; set; }

		public DbSet<CrawlableUrlEntity> CrawlableUrls { get; set; }

		public Context(string connectionString)
			: base(connectionString) { }
	}
}
