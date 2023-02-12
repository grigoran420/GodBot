using Discord;
using GodBot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Text.Json;
using WebApplication5.Models;
using EmbedViewModel = WebApplication5.Models.EmbedViewModel;

namespace WebApplication5.Controllers
{
	public class CreateEmbedController : Controller
	{
		public IActionResult Index()
		{ 
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(EmbedViewModel w, string action)
		{
			if (action == "Open") 
			{
				
				return await EditEmbed(w);
			}
			if (action == "EditNow")
			{
				string s = JsonSerializer.Serialize(w);
				Logger.LogToFile("Info", $"Send function (SendNow). Param -> \n{s}");
				return await EditNow(w);
			}
			if (action == "Send")
			{
				string[] files = Directory.GetFiles("Embeds", "*.json");

				//Models.EmbedViewModel Model = new();
				foreach (string x in files)
				{
					using (var f = new StreamReader(x))
					{
						GodBot.EmbedModel tempModel = new();
						string s = f.ReadToEnd();
						tempModel = JsonSerializer.Deserialize<GodBot.EmbedModel>(s);
						//SendedMessages sendedMessages = new() { ID = tempModel.messageID, channelName = tempModel.channelName };
						SelectListItem item = new SelectListItem()
						{
							Value = tempModel.messageID.ToString(),
							Text = tempModel.channelName.ToString(),
						};

						w.sendedEmbed.Add(item);
					}
				}
				if (w.sendedEmbed.Count != 0)
					w.sendedEmbed.First().Selected = true;

				if (w == null) { return View(w); }
				EmbedModel Embed = new()
				{
					Title = w.Title,
					Description = w.Description,
					Url = w.Url,
					authorName = w.authorName,
					Icon = w.Icon,
					authorLink = w.authorLink,
					authorIcon = w.authorIcon,
					footerText = w.footerText,
					footerUrl = w.footerUrl,
					ChannelId = ulong.Parse(w.channelId),
				};
				var ColorConvert = new ColorConverter();
				Embed.Color = (System.Drawing.Color)ColorConvert.ConvertFromString(w.Color);
				var temp = GodBot.SearchChannels.Channels();
				foreach (var tem in temp)
				{
					SelectListItem item = new SelectListItem()
					{
						Value = tem.ChannelId.ToString(),
						Text = tem.ChannelName.ToString(),
					};
					w.channelViewModels.Add(item);

					if (tem.ChannelId == Embed.ChannelId) Embed.channelName = tem.ChannelName;
				}
				if (w.channelViewModels.Count != 0)
					w.channelViewModels.First().Selected = true;
				var sendEmbed = new GodBot.sendEmbed();
				Embed.Filds = new()
				{
					new Fild {fildName = w.fildName1, fildText = w.fildValue1, fildInline = false},
					new Fild {fildName = w.fildName2, fildText = w.fildValue2, fildInline = false},
					new Fild {fildName = w.fildName3, fildText = w.fildValue3, fildInline = false},
					new Fild {fildName = w.fildName4, fildText = w.fildValue4, fildInline = false},
					new Fild {fildName = w.fildName5, fildText = w.fildValue5, fildInline = false},
					new Fild {fildName = w.fildName6, fildText = w.fildValue6, fildInline = false},
					new Fild {fildName = w.fildName7, fildText = w.fildValue7, fildInline = false},
					new Fild {fildName = w.fildName8, fildText = w.fildValue8, fildInline = false},
					new Fild {fildName = w.fildName9, fildText = w.fildValue9, fildInline = false},
					new Fild {fildName = w.fildName10, fildText = w.fildValue10, fildInline = false},
					new Fild {fildName = w.fildName11, fildText = w.fildValue11, fildInline = false},
					new Fild {fildName = w.fildName12, fildText = w.fildValue12, fildInline = false},
				};
				if (w.fildInline1 == "true") Embed.Filds[0].fildInline = true;
				if (w.fildInline2 == "true") Embed.Filds[1].fildInline = true;
				if (w.fildInline3 == "true") Embed.Filds[2].fildInline = true;
				if (w.fildInline4 == "true") Embed.Filds[3].fildInline = true;
				if (w.fildInline5 == "true") Embed.Filds[4].fildInline = true;
				if (w.fildInline6 == "true") Embed.Filds[5].fildInline = true;
				if (w.fildInline7 == "true") Embed.Filds[6].fildInline = true;
				if (w.fildInline8 == "true") Embed.Filds[7].fildInline = true;
				if (w.fildInline9 == "true") Embed.Filds[8].fildInline = true;
				if (w.fildInline10 == "true") Embed.Filds[9].fildInline = true;
				if (w.fildInline11 == "true") Embed.Filds[10].fildInline = true;
				if (w.fildInline12 == "true") Embed.Filds[11].fildInline = true;
				Logger.LogToFile("Info", $"Send Embed. Param -> \n{JsonSerializer.Serialize(Embed)}");
				sendEmbed.Send(Embed);
			}
			return View(w);
		}

		[HttpPost]
		public async Task<IActionResult> EditEmbed(EmbedViewModel w)
		{
			EmbedViewModel Model = new();
			string[] files = Directory.GetFiles("Embeds", "*.json");

			//Models.EmbedViewModel Model = new();
			foreach (string x in files)
			{
				using (var f = new StreamReader(x))
				{
					GodBot.EmbedModel tempModel = new();
					string s = f.ReadToEnd();
					tempModel = JsonSerializer.Deserialize<GodBot.EmbedModel>(s);
					//SendedMessages sendedMessages = new() { ID = tempModel.messageID, channelName = tempModel.channelName };
					SelectListItem item = new SelectListItem()
					{
						Value = tempModel.messageID.ToString(),
						Text = tempModel.channelName.ToString(),
					};
					if (w.EmbedID == item.Value)
					{
						Model.authorIcon = tempModel.authorIcon;
						Model.authorName = tempModel.authorName;
						Model.authorLink = tempModel.authorLink;
						Model.Description = tempModel.Description;
						Model.Title = tempModel.Title;
						Model.footerText = tempModel.footerText;
						Model.footerUrl = tempModel.footerUrl;
						Model.Icon = tempModel.Icon;
						Model.Url = tempModel.Url;
						Model.fildName1 = tempModel.Filds[0].fildName; Model.fildValue1 = tempModel.Filds[0].fildText;
						Model.fildName2 = tempModel.Filds[1].fildName; Model.fildValue2 = tempModel.Filds[1].fildText;
						Model.fildName3 = tempModel.Filds[2].fildName; Model.fildValue3 = tempModel.Filds[2].fildText;
						Model.fildName4 = tempModel.Filds[3].fildName; Model.fildValue4 = tempModel.Filds[3].fildText;
						Model.fildName5 = tempModel.Filds[4].fildName; Model.fildValue5 = tempModel.Filds[4].fildText;
						Model.fildName6 = tempModel.Filds[5].fildName; Model.fildValue6 = tempModel.Filds[5].fildText;
						Model.fildName7 = tempModel.Filds[6].fildName; Model.fildValue7 = tempModel.Filds[6].fildText;
						Model.fildName8 = tempModel.Filds[7].fildName; Model.fildValue8 = tempModel.Filds[7].fildText;
						Model.fildName9 = tempModel.Filds[8].fildName; Model.fildValue9 = tempModel.Filds[8].fildText;
						Model.fildName10 = tempModel.Filds[9].fildName; Model.fildValue10 = tempModel.Filds[9].fildText;
						Model.fildName11 = tempModel.Filds[10].fildName; Model.fildValue11 = tempModel.Filds[10].fildText;
						Model.fildName12 = tempModel.Filds[11].fildName; Model.fildValue12 = tempModel.Filds[11].fildText;
					}
					Model.sendedEmbed.Add(item);
				}
			}
			if (Model.sendedEmbed.Count != 0)
				Model.sendedEmbed.First().Selected = true;
			var temp = GodBot.SearchChannels.Channels();
			foreach (var tem in temp)
			{
				SelectListItem item = new SelectListItem()
				{
					Value = tem.ChannelId.ToString(),
					Text = tem.ChannelName.ToString(),
				};
				Model.channelViewModels.Add(item);
			}
			if (Model.channelViewModels.Count != 0)
				Model.channelViewModels.First().Selected = true;

			Logger.LogToFile("Info", $"Open Embed. Param -> \n{JsonSerializer.Serialize(Model)}");
			return View(Model);
		}

		[HttpPost]
		public async Task<IActionResult> EditNow(EmbedViewModel w)
		{
			string[] files = Directory.GetFiles("Embeds", "*.json");

			string channelName = "";
			//Models.EmbedViewModel Model = new();
			foreach (string x in files)
			{
				using (var f = new StreamReader(x))
				{
					GodBot.EmbedModel tempModel = new();
					string s = f.ReadToEnd();
					tempModel = JsonSerializer.Deserialize<GodBot.EmbedModel>(s);
					//SendedMessages sendedMessages = new() { ID = tempModel.messageID, channelName = tempModel.channelName };
					SelectListItem item = new SelectListItem()
					{
						Value = tempModel.messageID.ToString(),
						Text = tempModel.channelName.ToString(),
					};
					if (w.EmbedID == tempModel.messageID.ToString()) channelName = tempModel.channelName;
					w.sendedEmbed.Add(item);
				}
			}
			if (w.sendedEmbed.Count != 0)
				w.sendedEmbed.First().Selected = true;
			
			if (w == null) { return View(w); }
			EmbedModel Embed = new()
			{
				Title = w.Title,
				Description = w.Description,
				Url = w.Url,
				authorName = w.authorName,
				Icon = w.Icon,
				authorLink = w.authorLink,
				authorIcon = w.authorIcon,
				footerText = w.footerText,
				footerUrl = w.footerUrl,
				//ChannelId = ulong.Parse(w.channelId),
				messageID = ulong.Parse(w.EmbedID),
			};

			var ColorConvert = new ColorConverter();
			Embed.Color = (System.Drawing.Color)ColorConvert.ConvertFromString(w.Color);
			var temp = GodBot.SearchChannels.Channels();
			foreach (var tem in temp)
			{
				SelectListItem item = new SelectListItem()
				{
					Value = tem.ChannelId.ToString(),
					Text = tem.ChannelName.ToString(),
				};
				w.channelViewModels.Add(item);

				if (channelName == tem.ChannelName) Embed.ChannelId = tem.ChannelId;
				if (tem.ChannelId == Embed.ChannelId) Embed.channelName = tem.ChannelName;
			}
			if (w.channelViewModels.Count != 0)
				w.channelViewModels.First().Selected = true;
			var sendEmbed = new GodBot.sendEmbed();
			Embed.Filds = new()
			{
				new Fild {fildName = w.fildName1, fildText = w.fildValue1, fildInline = false},
				new Fild {fildName = w.fildName2, fildText = w.fildValue2, fildInline = false},
				new Fild {fildName = w.fildName3, fildText = w.fildValue3, fildInline = false},
				new Fild {fildName = w.fildName4, fildText = w.fildValue4, fildInline = false},
				new Fild {fildName = w.fildName5, fildText = w.fildValue5, fildInline = false},
				new Fild {fildName = w.fildName6, fildText = w.fildValue6, fildInline = false},
				new Fild {fildName = w.fildName7, fildText = w.fildValue7, fildInline = false},
				new Fild {fildName = w.fildName8, fildText = w.fildValue8, fildInline = false},
				new Fild {fildName = w.fildName9, fildText = w.fildValue9, fildInline = false},
				new Fild {fildName = w.fildName10, fildText = w.fildValue10, fildInline = false},
				new Fild {fildName = w.fildName11, fildText = w.fildValue11, fildInline = false},
				new Fild {fildName = w.fildName12, fildText = w.fildValue12, fildInline = false},
			};
			if (w.fildInline1 == "true") Embed.Filds[0].fildInline = true;
			if (w.fildInline2 == "true") Embed.Filds[1].fildInline = true;
			if (w.fildInline3 == "true") Embed.Filds[2].fildInline = true;
			if (w.fildInline4 == "true") Embed.Filds[3].fildInline = true;
			if (w.fildInline5 == "true") Embed.Filds[4].fildInline = true;
			if (w.fildInline6 == "true") Embed.Filds[5].fildInline = true;
			if (w.fildInline7 == "true") Embed.Filds[6].fildInline = true;
			if (w.fildInline8 == "true") Embed.Filds[7].fildInline = true;
			if (w.fildInline9 == "true") Embed.Filds[8].fildInline = true;
			if (w.fildInline10 == "true") Embed.Filds[9].fildInline = true;
			if (w.fildInline11 == "true") Embed.Filds[10].fildInline = true;
			if (w.fildInline12 == "true") Embed.Filds[11].fildInline = true;

			sendEmbed.EditNow(Embed);
			return View(w);
		}
	}
}
