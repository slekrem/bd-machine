namespace bd.machine.dal.Implementations.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("UrlRawHtml")]
	public class UrlRawHtml
	{
		[Key]
		public int Id { get; set; }
		
		public int UrlId { get; set; }

		public int RawHtmlId { get; set; }

		public DateTime CreationDate { get; set; }

		[ForeignKey("UrlId")]
		public virtual Url Url { get; set; }

		[ForeignKey("RawHtmlId")]
		public virtual RawHtml RawHtml { get; set; }
	}
}
