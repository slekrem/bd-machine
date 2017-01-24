namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IPathRepository
	{
		IQueryable<Path> Paths { get; }

		void Create(Path path);
	}
}
