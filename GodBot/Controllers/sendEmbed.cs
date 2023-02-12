using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord.Interactions;
using Discord.Interactions.Builders;
using Discord.Net.Queue;
using Discord.Net.Rest;
using Discord.Rest;
using Discord.WebSocket;
using GodBot.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GodBot
{
	public class sendEmbed
	{
		public sendEmbed() {
		}
		EmbedBuilder builder;
		CommandService commands;
		public async void Send(EmbedModel model, SocketSlashCommand? command = null)
		{
			EmbedBuilder builder = new EmbedBuilder() 
			{
				Title= model.Title,
				Description= model.Description,
				ImageUrl = model.Url,
				Color = new Color(model.Color.R, model.Color.G, model.Color.B),
				ThumbnailUrl = model.Icon,
				Author = new EmbedAuthorBuilder() 
				{ 
					Name = model.authorName,
					Url = model.authorLink,
					IconUrl = model.authorIcon,
				},
				Footer = new EmbedFooterBuilder()
				{ 
					Text = model.footerText,
					IconUrl = model.footerUrl
				}
			};

			foreach (var x in model.Filds)
			{
				if (x.fildName != null & x.fildText != null) 
				{
					if (x.fildName.Replace(" ", "") != "" & x.fildText.Replace(" ", "") != "")
					{
						builder.AddField(x.fildName, x.fildText, x.fildInline);
						continue;
					}
				}
			}

			//Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(EmbedModel.ChannelId).SendMessageAsync(embed: builder.Build());
			var UserMessage = await Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/)
				.GetTextChannel(model.ChannelId)
				.SendMessageAsync(embed: builder.Build());
			List<IReadOnlyCollection<IMessage>> message = new();
			ulong id = 0;
			message = await Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/)
				.GetTextChannel(model.ChannelId)
				.GetMessagesAsync(10)
				.ToListAsync();
			//builder.ThumbnailUrl = "https://i.guim.co.uk/img/media/c2c59285912ea176646bc23d00ca5464aabfecff/0_27_640_384/master/640.jpg?width=620&quality=85&dpr=1&s=none";
			/*MessageProperties msg= new MessageProperties();
			Action<MessageProperties> action;
			msg.Embed = new Optional<Embed>(builder.Build());
			action = msg.;
			action.BeginInvoke(msg);*/
			//model.userMessage = UserMessage;
			foreach (var messageItem in message)
			{ 
				foreach (var item in messageItem)
				{
					
					if (item.Author.Id == 1020045184907620403)
					{
						id = item.Id;
						break;
					}
				}
			}
			//await Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(model.ChannelId).ModifyMessageAsync(id, action);
			model.messageID = id;
			using (var f = new FileStream($"Embeds/{id}.json", FileMode.Create)) { f.Close(); };
			using (var f = new StreamWriter($"Embeds/{id}.json", false)) 
			{
				string s = JsonSerializer.Serialize(model);
				await f.WriteLineAsync(s);
				Console.WriteLine(s);
				f.Close(); 
			};

		}

		public async void EditNow(EmbedModel model, SocketSlashCommand? command = null)
		{
			EmbedBuilder builder = new EmbedBuilder()
			{
				Title = model.Title,
				Description = model.Description,
				ImageUrl = model.Url,
				Color = new Color(model.Color.R, model.Color.G, model.Color.B),
				ThumbnailUrl = model.Icon,
				Author = new EmbedAuthorBuilder()
				{
					Name = model.authorName,
					Url = model.authorLink,
					IconUrl = model.authorIcon,
				},
				Footer = new EmbedFooterBuilder()
				{
					Text = model.footerText,
					IconUrl = model.footerUrl
				}
			};

			foreach (var x in model.Filds)
			{
				if (x.fildName != null & x.fildText != null)
				{
					if (x.fildName.Replace(" ", "") != "" & x.fildText.Replace(" ", "") != "")
					{
						builder.AddField(x.fildName, x.fildText, x.fildInline);
						continue;
					}
				}
			}

			//Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(EmbedModel.ChannelId).SendMessageAsync(embed: builder.Build());
			/*await Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение)
				.GetTextChannel(model.ChannelId)
				.SendMessageAsync(embed: builder.Build());*/

			MessageProperties msg = new MessageProperties();
			Action<MessageProperties> action = null;
			msg.Embed = new Optional<Embed>(builder.Build());
			//action.Invoke(msg); 

			await Action._client.GetGuild(805711346620432384).GetTextChannel(model.ChannelId).ModifyMessageAsync(model.messageID, x => x.Embed = builder.Build());
			ulong id = 0;

			var message = await Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/)
				.GetTextChannel(model.ChannelId)
				.GetMessagesAsync(10)
				.ToListAsync();
			//builder.ThumbnailUrl = "https://i.guim.co.uk/img/media/c2c59285912ea176646bc23d00ca5464aabfecff/0_27_640_384/master/640.jpg?width=620&quality=85&dpr=1&s=none";
			/*MessageProperties msg= new MessageProperties();
			Action<MessageProperties> action;
			msg.Embed = new Optional<Embed>(builder.Build());
			action = msg.;
			action.BeginInvoke(msg);*/
			//model.userMessage = UserMessage;
			foreach (var messageItem in message)
			{
				foreach (var item in messageItem)
				{

					if (item.Author.Id == 1020045184907620403)
					{
						id = item.Id;
						break;
					}
				}
			}
			//await Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(model.ChannelId).ModifyMessageAsync(id, action);
			model.messageID = id;
			using (var f = new StreamWriter($"Embeds/{model.messageID}.json", false))
			{
				string s = JsonSerializer.Serialize(model);
				await f.WriteLineAsync(s);
				Console.WriteLine(s);
				f.Close();
			};

		}

		bool CheckInline(string s)
		{
			if(s == "true") return true;
			return false;
		}

	}
}
