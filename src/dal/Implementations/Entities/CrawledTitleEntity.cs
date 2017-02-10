namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("CrawledTitle")]
	public class CrawledTitleEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawHtmlId { get; set; }

		[Required]
		public int RawTitleId { get; set; }

		[Required]
		public DateTime UtcTimestamp { get; set; }

		[ForeignKey("RawHtmlId")]
		public virtual RawHtmlEntity RawHtml { get; set; }

		[ForeignKey("RawTitleId")]
		public virtual RawTitleEntity RawTitle { get; set; }
	}
}
