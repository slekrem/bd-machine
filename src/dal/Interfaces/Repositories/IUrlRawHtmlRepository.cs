namespace bd.machine.dal.Interfaces.Repositories
{
	using System.Linq;
	using Implementations.Models;

	public interface IUrlRawHtmlRepository
	{
		IQueryable<UrlRawHtml> UrlRawHtml { get; }
		
		void Create(UrlRawHtml urlRawHtml);
	}
}
