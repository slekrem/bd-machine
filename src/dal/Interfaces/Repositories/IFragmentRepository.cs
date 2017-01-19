namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IFragmentRepository
	{
		IQueryable<Fragment> Fragments { get; }

		void Create(Fragment fragment);
	}
}
