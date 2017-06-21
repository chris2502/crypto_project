using System;
using ServiceStack.ServiceHost;
using ServiceStack;
using ServiceStack.Text;

namespace DevisBack.Tools.StaticTools
{
	public static class ComplexeRequest
	{
	
		public static void PopulateRequest<T>(ref T request, IHttpRequest requestDto) 
		{
			var jsonParam = requestDto.GetParam("Data");
			if (jsonParam != null)
				request = JsonSerializer.DeserializeFromString<T> (jsonParam);	
		}
			
	}
}

