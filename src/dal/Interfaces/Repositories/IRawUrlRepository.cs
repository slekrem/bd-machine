namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Entities;

	public interface IRawUrlRepository
	{
		IQueryable<RawUrlEntity> RawUrls { get; }

		void Create(RawUrlEntity rawUrlEntity);
	}
}
