namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class PortRepository : IPortRepository
	{
		private readonly IContext _context;

		public IQueryable<Port> Ports
		{
			get
			{
				return _context.Ports;
			}
		}

		public PortRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(Port port)
		{
			if (port == null)
				throw new ArgumentNullException("port");
			_context.Ports.Add(port);
			_context.SaveChanges();
		}
	}
}
