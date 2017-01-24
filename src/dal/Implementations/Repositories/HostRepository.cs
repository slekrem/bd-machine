namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class HostRepository : IHostRepository
	{
		private readonly IContext _context;

		public IQueryable<Host> Hosts
		{
			get
			{
				return _context.Hosts;
			}
		}

		public HostRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(Host host)
		{
			if (host == null)
				throw new ArgumentNullException("host");
			_context.Hosts.Add(host);
			_context.SaveChanges();
		}
	}
}
