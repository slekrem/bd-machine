namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	
	public class QueryRepository : IQueryRepository
	{
		private readonly IContext _context;

		public QueryRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
