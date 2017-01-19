namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IUrlMetadataRepository
	{
		IQueryable<UrlMetadata> UrlMetadatas { get; }
	}
}
