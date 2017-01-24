namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Entities;

	public interface IRawHostRepository
	{
		IQueryable<RawHostEntity> RawHosts { get; }

		void Create(RawHostEntity rawHostEntity);
	}
}
