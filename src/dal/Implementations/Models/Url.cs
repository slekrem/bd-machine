namespace bd.machine.dal.Implementations.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("Url")]
	public class Url
	{
		[Key]
		public int Id { get; set; }

		public int SchemeId { get; set; }

		public int HostId { get; set; }

		public int PortId { get; set; }

		public int PathId { get; set; }

		public Nullable<int> QueryId { get; set; }

		public Nullable<int> FragmentId { get; set; }

		public int UrlMetadataId { get; set; }

		[ForeignKey("SchemeId")]
		public virtual Scheme Scheme { get; set; }

		[ForeignKey("HostId")]
		public virtual Host Host { get; set; }

		[ForeignKey("PortId")]
		public virtual Port Port { get; set; }

		[ForeignKey("PathId")]
		public virtual Path Path { get; set; }

		[ForeignKey("QueryId")]
		public virtual Query Query { get; set;}

		[ForeignKey("FragmentId")]
		public virtual Fragment Fragment { get; set; }

		[ForeignKey("UrlMetadataId")]
		public virtual UrlMetadata UrlMetadata { get; set; }
	}
}