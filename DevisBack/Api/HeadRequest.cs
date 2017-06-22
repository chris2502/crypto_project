using System;
using ServiceStack.DataAnnotations;

namespace DevisBack.Api
{
	// All resquest class should inherit this class to check authentication before any requestDto
	public class HeadRequest
	{
		public string Token{ get; set;}
	}
}

