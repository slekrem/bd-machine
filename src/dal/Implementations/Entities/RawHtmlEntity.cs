namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawHtml")]
	public class RawHtmlEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawUrlId { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }

		[Required]
		public byte[] Data { get; set; }

		[Required]
		public bool CrawledHosts { get; set; }

		[Required]
		public bool CrawledUrls { get; set; }

		[Required]
		public bool CrawledTitle { get; set; }

		[Required]
		public bool CrawledDescription { get; set; }

		[ForeignKey("RawUrlId")]
		public virtual RawUrlEntity RawUrl { get; set; }
	}
}
