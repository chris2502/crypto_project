using System;
using ServiceStack.DataAnnotations;

namespace DevisBack.Api
{
	// All resquest class should inherit this class to check authentication before any requestDto
	public class HeadRequest
	{
        public int? Id { get; set; }
		public string Token{ get; set;}
	}
}

