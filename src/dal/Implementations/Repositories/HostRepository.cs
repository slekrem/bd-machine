namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;

	public class HostRepository : IHostRepository
	{
		private readonly IContext _context;

		public HostRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
