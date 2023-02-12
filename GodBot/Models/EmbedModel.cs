using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.Json.Serialization;
using Discord.Rest;

namespace GodBot
{
	public class EmbedModel
	{
		[JsonInclude]
		public string Title { get; set; } = "";
		[JsonInclude]
		public string Description { get; set; } = "";
		[JsonInclude]
		public string Url { get; set; } = "";
		[JsonInclude]
		public System.Drawing.Color Color { get; set; } = new System.Drawing.Color();
		[JsonInclude]
		public string Icon { get; set; } = "";
		[JsonInclude]
		public string authorName { get; set; } = "";
		[JsonInclude]
		public string authorLink { get; set; } = "";
		[JsonInclude]
		public string authorIcon { get; set; } = "";
		[JsonInclude]
		public List<Fild> Filds { get; set; } = new List<Fild>();
		[JsonInclude]
		public string footerText { get; set; } = "";
		[JsonInclude]
		public string footerUrl { get; set; } = "";
		[JsonInclude]
		public ulong ChannelId { get; set; } = 0;
		[JsonInclude]
		public ulong messageID { get; set; } = 0;
		[JsonInclude]
		public string channelName { get; set; }
		//[JsonInclude]
		//public RestUserMessage userMessage { get; set; }
		public EmbedModel() { }
	}
}
