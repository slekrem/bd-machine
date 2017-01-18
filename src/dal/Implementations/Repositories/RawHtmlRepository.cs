namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class RawHtmlRepository : IRawHtmlRepository
	{
		private readonly IContext _context;
		
		public RawHtmlRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(RawHtml rawHtml)
		{
			if (rawHtml == null)
				throw new ArgumentNullException("rawHtml");
			_context.RawHtmls.Add(rawHtml);
			_context.SaveChanges();
		}
	}
}
