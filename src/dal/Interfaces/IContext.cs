namespace bd.machine.dal.Interfaces
{
	using System;
	using System.Data.Entity;
	using Implementations.Entities;
	using Implementations.Models;

	public interface IContext : IDisposable
	{
		DbSet<Scheme> Schemes { get; set; }

		DbSet<Host> Hosts { get; set; }

		DbSet<Path> Paths { get; set; }

		DbSet<Port> Ports { get; set; }

		DbSet<Query> Queries { get; set; }

		DbSet<Fragment> Fragments { get; set; }

		DbSet<Url> Urls { get; set; }

		DbSet<UrlMetadata> UrlMetadatas { get; set; }

		DbSet<RawHtmlEntity> RawHtmls { get; set; }

		DbSet<UrlRawHtml> UrlRawHtml { get; set; }

		DbSet<RawHostEntity> RawHosts { get; set; }

		DbSet<RawUrlEntity> RawUrls { get; set; }

		DbSet<CrawlableUrlEntity> CrawlableUrls { get; set; }

		int SaveChanges();
	}
}
