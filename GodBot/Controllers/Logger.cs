using Discord;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GodBot
{
	public class Logger
	{

		public static void LogToFile(LogMessage msg)
		{
			try
			{
				using (var fs = new StreamWriter("temp.log", true))
				{
					fs.WriteLine(msg.ToString());
					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				using (var fs = File.Create("temp.log")) ;
				using (var fs = new StreamWriter("temp.log", true))
				{
					fs.WriteLine(msg.ToString());
				}
			}
		}

		public static void LogToFile(string sender, string msg)
		{
			try
			{
				using (var fs = new StreamWriter("temp.log", true))
				{
					fs.WriteLine($"{DateTime.Today.ToShortDateString()} " +
						$"{DateTime.Now.Hour}:" +
						$"{DateTime.Now.Minute}:" +
						$"{DateTime.Now.Second} -> " +
						$"{sender}: {msg}");

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				using (var fs = File.Create("temp.log")) ;
				using (var fs = new StreamWriter("temp.log", true))
				{
					fs.WriteLine($"{DateTime.Today.ToShortDateString()} " +
						$"{DateTime.Now.Hour}:" +
						$"{DateTime.Now.Minute}:" +
						$"{DateTime.Now.Second} -> " +
						$"{sender}: {msg}");
				}
			}
		}

		public static Task Log(LogMessage msg)
		{
			LogToFile(msg);
			return Task.CompletedTask;
		}
	}
}
