namespace bd.machine.dal.Implementations.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("UrlMetadata")]
	public class UrlMetadata
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }
	}
}
