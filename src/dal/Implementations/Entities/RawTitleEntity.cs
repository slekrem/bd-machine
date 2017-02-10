namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawTitle")]
	public class RawTitleEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int RawUrlId { get; set; }

		[Required]
		public DateTime UtcTimestamp { get; set; }

		[Required]
		public string Data { get; set; }

		[ForeignKey("RawUrlId")]
		public virtual RawUrlEntity RawUrl { get; set; }
	}
}
