using System;

namespace PROG7312_ST10204001_I_Lodewyk_POE_Part_1_Municipal_Services.Helpers
{
	public static class Logger
	{
		public static void Log(string message)
		{
			Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
		}
	}

}
