﻿namespace bd.machine.bal.Implementations.Models
{
	using System;

	public class UrlRequest
	{
		public int Id { get; set; }

		public DateTime RequestDateTime { get; set; }

		public float TrafficInKilobyte { get; set; }

		public int RawHtmlId { get; set; }
	}
}
