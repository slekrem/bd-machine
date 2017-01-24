namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IQueryRepository
	{
		IQueryable<Query> Queries { get; }

		void Create(Query query);
	}
}
