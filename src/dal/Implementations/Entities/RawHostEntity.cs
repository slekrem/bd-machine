namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawHost")]
	public class RawHostEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }

		[Required]
		public string Data { get; set; }
	}
}
