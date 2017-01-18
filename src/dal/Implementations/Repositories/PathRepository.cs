namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	
	public class PathRepository : IPathRepository
	{
		private readonly IContext _context;

		public PathRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
