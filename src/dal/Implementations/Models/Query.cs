namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Query")]
	public class Query
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		public string Value { get; set; }
	}
}
