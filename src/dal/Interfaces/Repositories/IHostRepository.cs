namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IHostRepository
	{
		IQueryable<Host> Hosts { get; }

		void Create(Host host);
	}
}
