namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("CrawledUrl")]
	public class CrawledUrlEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawHtmlId { get; set; }

		[Required]
		public int RawUrlId { get; set; }

		[Required]
		public DateTime UtcTimestamp { get; set; }

		[ForeignKey("RawHtmlId")]
		public RawHtmlEntity RawHtml { get; set; }

		[ForeignKey("RawUrlId")]
		public RawUrlEntity RawUrl { get; set; }
	}
}
