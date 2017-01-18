namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Port")]
	public class Port
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		public int Value { get; set; }
	}
}
