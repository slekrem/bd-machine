namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class QueryRepository : IQueryRepository
	{
		private readonly IContext _context;

		public IQueryable<Query> Queries
		{
			get
			{
				return _context.Queries;
			}
		}

		public QueryRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(Query query)
		{
			if (query == null)
				throw new ArgumentNullException("query");
			_context.Queries.Add(query);
			_context.SaveChanges();
		}
	}
}
