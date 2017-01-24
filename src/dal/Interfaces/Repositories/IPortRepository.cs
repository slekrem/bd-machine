namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IPortRepository
	{
		IQueryable<Port> Ports { get; }

		void Create(Port port);
	}
}
