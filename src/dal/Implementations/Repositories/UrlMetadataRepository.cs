namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using System.Linq;
	using Models;
	using Interfaces;
	using Interfaces.Repositories;

	public class UrlMetadataRepository : IUrlMetadataRepository
	{
		private readonly IContext _context;

		public IQueryable<UrlMetadata> UrlMetadatas
		{
			get
			{
				return _context.UrlMetadatas;
			}
		}

		public UrlMetadataRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}
	}
}
