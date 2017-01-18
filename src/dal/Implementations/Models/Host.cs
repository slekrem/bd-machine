namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Host")]
	public class Host
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		public string Value { get; set; }
	}
}
