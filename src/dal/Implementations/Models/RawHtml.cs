namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("RawHtml")]
	public class RawHtml
	{
		[Key]
		public int Id { get; set; }

		public byte[] Value { get; set; }
	}
}
