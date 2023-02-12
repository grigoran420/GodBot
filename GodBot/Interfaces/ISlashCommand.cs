using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBot.Interfaces
{
	public interface ISlashCommand
	{
		public static async Task SlashCommand()
		{
			throw new NotImplementedException();
		}
		public static async Task SlashCommandHandler(SocketSlashCommand context)
		{
			throw new NotImplementedException();
		}

	}
}
