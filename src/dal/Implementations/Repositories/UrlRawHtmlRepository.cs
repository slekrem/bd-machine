namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class UrlRawHtmlRepository : IUrlRawHtmlRepository
	{
		private readonly IContext _context;
		
		public UrlRawHtmlRepository(IContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		public void Create(UrlRawHtml urlRawHtml)
		{
			if (urlRawHtml == null)
				throw new ArgumentNullException("urlRawHtml");
			_context.UrlRawHtml.Add(urlRawHtml);
			_context.SaveChanges();
		}
	}
}
