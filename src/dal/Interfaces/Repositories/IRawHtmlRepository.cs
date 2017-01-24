namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Entities;

	public interface IRawHtmlRepository
	{
		IQueryable<RawHtmlEntity> RawHtmls { get; }
		
		void Create(RawHtmlEntity rawHtml);
	}
}
