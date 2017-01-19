namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IUrlRepository
	{
		IQueryable<Url> Urls { get; }

		void Create(Url url);
	}
}
