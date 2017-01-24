namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawUrl")]
	public class RawUrl
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Value { get; set; }
	}
}
