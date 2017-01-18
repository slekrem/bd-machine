namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	
	public class SchemeRepository : ISchemeRepository
	{
		private readonly IContext _context;

		public SchemeRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
