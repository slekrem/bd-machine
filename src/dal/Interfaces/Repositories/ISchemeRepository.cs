namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface ISchemeRepository
	{
		IQueryable<Scheme> Schemes { get; }

		void Create(Scheme scheme);
	}
}
