namespace bd.machine.dal.Implementations.Repositories
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	
	public class UrlMetadataRepository : IUrlMetadataRepository
	{
		private readonly IContext _context;

		public UrlMetadataRepository(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");

			_context = context;
		}
	}
}
