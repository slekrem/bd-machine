namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("CrawledHost")]
	public class CrawledHostEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawHtmlId { get; set; }

		[Required]
		public int RawHostId { get; set; }

		[Required]
		public DateTime UtcTimestamp { get; set; }

		[ForeignKey("RawHtmlId")]
		public virtual RawHtmlEntity RawHtml { get; set; }

		[ForeignKey("RawHostId")]
		public virtual RawHostEntity RawHost { get; set; }
	}
}
