namespace bd.machine.bal.Interfaces
{
	using System;
	using System.Collections.Generic;

	public interface IUrlService
	{
		IDictionary<int, Uri> GetAllUris();
	}
}
