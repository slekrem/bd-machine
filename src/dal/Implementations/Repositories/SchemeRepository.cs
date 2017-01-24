namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class SchemeRepository : ISchemeRepository
	{
		private readonly IContext _context;

		public IQueryable<Scheme> Schemes
		{
			get
			{
				return _context.Schemes;
			}
		}

		public SchemeRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}

		public void Create(Scheme scheme)
		{
			if (scheme == null)
				throw new ArgumentNullException("scheme");
			_context.Schemes.Add(scheme);
			_context.SaveChanges();
		}
	}
}
