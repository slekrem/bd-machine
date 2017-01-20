namespace bd.machine.dal.Implementations
{
	using System;
	using Interfaces;
	using Interfaces.Repositories;
	using Repositories;

	public class UnitOfWork : IUnitOfWork
	{
		private readonly IContext _context;

		private IFragmentRepository _fragmentRepository;
		private IHostRepository _hostRepository;
		private IPathRepository _pathRepository;
		private IPortRepository _portRepository;
		private IRawHtmlRepository _rawHtmlRepository;
		private IQueryRepository _queryRepository;
		private ISchemeRepository _schemeRepository;
		private IUrlMetadataRepository _urlMetadataRepository;
		private IUrlRepository _urlRepository;
		private IUrlRawHtmlRepository _urlRawHtmlRepository;
		private IRawHostRepository _rawHostRepository;
		private IRawUrlRepository _rawUrlRepository;
		
		public IFragmentRepository FragmentRepository 
		{ 
			get 
			{
				if (_fragmentRepository == null)
					_fragmentRepository = new FragmentRepository(_context);
				return _fragmentRepository;
			}
		}

		public IHostRepository HostRepository 
		{ 
			get 
			{
				if (_hostRepository == null)
					_hostRepository = new HostRepository(_context);
				return _hostRepository;
			}
		}

		public IPathRepository PathRepository 
		{ 
			get 
			{
				if (_pathRepository == null)
					_pathRepository = new PathRepository(_context);
				return _pathRepository;
			}
		}

		public IPortRepository PortRepository 
		{ 
			get 
			{
				if (_portRepository == null)
					_portRepository = new PortRepository(_context);
				return _portRepository;
			}
		}

		public IRawHtmlRepository RawHtmlRepository 
		{
			get 
			{
				if (_rawHtmlRepository == null)
					_rawHtmlRepository = new RawHtmlRepository(_context);
				return _rawHtmlRepository;
			}
		}

		public IQueryRepository QueryRepository 
		{ 
			get 
			{
				if (_queryRepository == null)
					_queryRepository = new QueryRepository(_context);
				return _queryRepository;
			}
		}

		public ISchemeRepository SchemeRepository 
		{ 
			get 
			{
				if (_schemeRepository == null)
					_schemeRepository = new SchemeRepository(_context);
				return _schemeRepository;
			}
		}

		public IUrlMetadataRepository UrlMetadataRepository 
		{ 
			get 
			{
				if (_urlMetadataRepository == null)
					_urlMetadataRepository = new UrlMetadataRepository(_context);
				return _urlMetadataRepository;
			}
		}

		public IUrlRepository UrlRepository 
		{ 
			get 
			{
				if (_urlRepository == null)
					_urlRepository = new UrlRepository(_context);
				return _urlRepository;
			}
		}

		public IUrlRawHtmlRepository UrlRawHtmlRepository 
		{
			get 
			{
				if (_urlRawHtmlRepository == null)
					_urlRawHtmlRepository = new UrlRawHtmlRepository(_context);
				return _urlRawHtmlRepository;
			}
		}

		public IRawHostRepository RawHostRepository
		{
			get
			{
				if (_rawHostRepository == null)
					_rawHostRepository = new RawHostRepository(_context);
				return _rawHostRepository;
			}
		}

		public IRawUrlRepository RawUrlRepository
		{
			get
			{
				if (_rawUrlRepository == null)
					_rawUrlRepository = new RawUrlRepository(_context);
				return _rawUrlRepository;
			}
		}

		public UnitOfWork(IContext context) 
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}
	}
}
