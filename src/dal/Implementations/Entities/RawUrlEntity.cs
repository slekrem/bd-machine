namespace bd.machine.dal.Implementations.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawUrl")]
	public class RawUrlEntity
	{
		[Key]
		public int Id { get; set; }

		public int RawHostId { get; set; }

		public DateTime Timestamp { get; set; }

		public string Data { get; set; }
	}
}
