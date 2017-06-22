using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Api
{
	// All response class should inherit this class to refactor response code
    public class AllResponse
    {
        public int Code { get; set; }
    }
}