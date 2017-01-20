namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IRawHtmlRepository
	{
		IQueryable<RawHtml> RawHtmls { get; }
		
		void Create(RawHtml rawHtml);
	}
}
