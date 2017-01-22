namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	using System.Linq;
	using Entities;

	public class RawHtmlRepository : IRawHtmlRepository
	{
		private readonly IContext _context;

		public IQueryable<RawHtmlEntity> RawHtmls
		{
			get
			{
				return _context.RawHtmls;
			}
		}
		
		public RawHtmlRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(RawHtmlEntity rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			_context.RawHtmls.Add(rawHtml);
			_context.SaveChanges();
		}
	}
}
