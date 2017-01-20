namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("CrawlableUrl")]
	public class CrawlableUrlEntity
	{
		[Key]
		public int Id { get; set; }

		public int RawUrlId { get; set; }

		public DateTime Timestamp { get; set; }

		public bool IsActivated { get; set; }

		public virtual RawUrlEntity RawUrl { get; set; }
	}
}
