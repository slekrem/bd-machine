namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class FragmentRepository : IFragmentRepository
	{
		private readonly IContext _context;

		public IQueryable<Fragment> Fragments
		{
			get
			{
				return _context.Fragments;
			}
		}

		public FragmentRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}

		public void Create(Fragment fragment)
		{
			if (fragment == null)
				throw new ArgumentNullException("fragment");
			_context.Fragments.Add(fragment);
			_context.SaveChanges();
		}
	}
}
