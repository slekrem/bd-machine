namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Entities;
	using Interfaces;
	using Interfaces.Repositories;

	public class RawHostRepository : IRawHostRepository
	{
		private readonly IContext _context;
		
		public IQueryable<RawHostEntity> RawHosts
		{
			get
			{
				return _context.RawHosts;
			}
		}
		
		public RawHostRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(RawHostEntity rawHostEntity)
		{
			if (rawHostEntity == null)
				throw new ArgumentNullException("rawHostEntity");
			_context.RawHosts.Add(rawHostEntity);
			_context.SaveChanges();
		}
	}
}
