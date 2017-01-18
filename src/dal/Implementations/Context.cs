namespace bd.machine.dal.Implementations
{
	using System.Data.Entity;
	using Models;
	using Interfaces;

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

		public DbSet<RawHtml> RawHtmls { get; set; }

		public DbSet<UrlRawHtml> UrlRawHtml { get; set; }

		public Context(string connectionString)
			: base(connectionString) { }
	}
}
