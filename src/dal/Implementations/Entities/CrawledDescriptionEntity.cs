namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("CrawledDescription")]
	public class CrawledDescriptionEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawHtmlId { get; set; }

		[Required]
		public int RawDescriptionId { get; set; }

		[Required]
		public DateTime UtcTimestamp { get; set; }
	}
}
