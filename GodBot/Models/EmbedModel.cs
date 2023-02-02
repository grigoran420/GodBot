using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GodBot
{
	public static class EmbedModel
	{
		public static string Title { get; set; } = "";
		public static string Description { get; set; } = "";
		public static string Url { get; set; } = "";
		public static System.Drawing.Color Color { get; set; } = new System.Drawing.Color();
		public static string Icon { get; set; } = "";
		public static string authorName { get; set; } = "";
		public static string authorLink { get; set; } = "";
		public static string authorIcon { get; set; } = "";
		public static List<Fild> Filds { get; set; } = new List<Fild>();
		public static string footerText { get; set; } = "";
		public static string footerUrl { get; set; } = "";
		public static ulong ChannelId { get; set; } = 0;
	}
}
