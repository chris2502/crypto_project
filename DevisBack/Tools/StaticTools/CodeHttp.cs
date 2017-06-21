using System;

namespace DevisBack.Tools.StaticTools
{
	public static class CodeHttp
	{
		//Success Code
		public const int OK = 200;
		public const int CREATED = 201;
		public const int NO_CONTENT = 204;

		//Client Error
		public const int BAD_REQUEST = 400;
		public const int UNAUTHORIZED = 401;
		public const int FORBIDEN = 403;
		public const int NOT_FOUND = 404;

		//Server Error
		public const int INTERNAL_SERVER_ERROR = 500;
		public const int NOT_IMPLEMENTED = 501;
		public const int SERVICE_UNAIVAILABLE = 503;
	}

}

