namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class UrlRepository : IUrlRepository
	{
		private readonly IContext _context;

		public IQueryable<Url> Urls
		{
			get
			{
				return _context.Urls;
			}
		}

		public UrlRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
