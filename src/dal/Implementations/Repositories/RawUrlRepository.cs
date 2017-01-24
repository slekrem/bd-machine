namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Entities;
	using Interfaces;
	using Interfaces.Repositories;

	public class RawUrlRepository : IRawUrlRepository
	{
		private readonly IContext _context;
		
		public IQueryable<RawUrlEntity> RawUrls
		{
			get
			{
				return _context.RawUrls;
			}
		}

		public RawUrlRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(RawUrlEntity rawUrlEntity)
		{
			if (rawUrlEntity == null)
				throw new ArgumentNullException("rawUrlEntity");
			_context.RawUrls.Add(rawUrlEntity);
			_context.SaveChanges();
		}
	}
}
