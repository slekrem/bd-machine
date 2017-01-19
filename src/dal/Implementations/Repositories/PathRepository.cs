namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class PathRepository : IPathRepository
	{
		private readonly IContext _context;

		public IQueryable<Path> Paths
		{
			get
			{
				return _context.Paths;;
			}
		}

		public PathRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(Path path)
		{
			if (path == null)
				throw new ArgumentNullException("path");
			_context.Paths.Add(path);
			_context.SaveChanges();
		}
	}
}
