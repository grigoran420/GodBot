using Discord;
using Discord.Commands;
using Discord.Commands.Builders;
using Discord.Interactions;
using Discord.Interactions.Builders;
using Discord.Net.Queue;
using Discord.Net.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBot
{
	public class sendEmbed
	{
		public sendEmbed() {
		}
		EmbedBuilder builder;
		CommandService commands;
		public async void Sand(EmbedViewModel model, SocketSlashCommand? command = null)
		{
			EmbedBuilder builder = new EmbedBuilder() 
			{
				Title= EmbedModel.Title,
				Description= EmbedModel.Description,
				ImageUrl = EmbedModel.Url,
				Color = new Color(EmbedModel.Color.R, EmbedModel.Color.G, EmbedModel.Color.B),
				ThumbnailUrl = EmbedModel.Icon,
				Author = new EmbedAuthorBuilder() 
				{ 
					Name = EmbedModel.authorName,
					Url = EmbedModel.authorLink,
					IconUrl = EmbedModel.authorIcon,
				},
				Footer = new EmbedFooterBuilder()
				{ 
					Text = EmbedModel.footerText,
					IconUrl = EmbedModel.footerUrl
				}
			};
			List<Fild> filds = new List<Fild>()
			{
				new Fild() {fildName = model.fildName1, fildText = model.fildValue1, fildInline = CheckInline(model.fildInline1)},
				new Fild() {fildName = model.fildName2, fildText = model.fildValue2, fildInline = CheckInline(model.fildInline2)},
				new Fild() {fildName = model.fildName3, fildText = model.fildValue3, fildInline = CheckInline(model.fildInline3)},
				new Fild() {fildName = model.fildName4, fildText = model.fildValue4, fildInline = CheckInline(model.fildInline4)},
				new Fild() {fildName = model.fildName5, fildText = model.fildValue5, fildInline = CheckInline(model.fildInline5)},
				new Fild() {fildName = model.fildName6, fildText = model.fildValue6, fildInline = CheckInline(model.fildInline6)},
				new Fild() {fildName = model.fildName7, fildText = model.fildValue7, fildInline = CheckInline(model.fildInline7)},
				new Fild() {fildName = model.fildName8, fildText = model.fildValue8, fildInline = CheckInline(model.fildInline8)},
				new Fild() {fildName = model.fildName9, fildText = model.fildValue9, fildInline = CheckInline(model.fildInline9)},
				new Fild() {fildName = model.fildName10, fildText = model.fildValue10, fildInline = CheckInline(model.fildInline10)},
				new Fild() {fildName = model.fildName11, fildText = model.fildValue11, fildInline = CheckInline(model.fildInline11)},
				new Fild() {fildName = model.fildName12, fildText = model.fildValue12, fildInline = CheckInline(model.fildInline12)},
			};

			foreach (var x in filds)
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

			Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(EmbedModel.ChannelId).SendMessageAsync(embed: builder.Build());
			//Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(EmbedModel.ChannelId).SendMessageAsync(embed: builder.Build());

		}

		bool CheckInline(string s)
		{
			if(s == "true") return true;
			return false;
		}

	}
}
