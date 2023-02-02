using GodBot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using WebApplication5.Models;
using EmbedViewModel = WebApplication5.Models.EmbedViewModel;

namespace WebApplication5.Controllers
{
	public class CreateEmbedController : Controller
	{
		[HttpPost]
		public async Task<IActionResult> Index(EmbedViewModel w)
		{
			
			
			if (w == null) { return View(w); }
			EmbedModel.Title = w.Title;
			EmbedModel.Description = w.Description;
			EmbedModel.Url = w.Url;
			EmbedModel.Icon = w.Icon;
			EmbedModel.authorName = w.authorName;
			EmbedModel.authorLink = w.authorLink;
			EmbedModel.authorIcon = w.authorIcon;
			EmbedModel.footerText = w.footerText;
			EmbedModel.footerUrl = w .footerUrl;
			EmbedModel.ChannelId = ulong.Parse(w.channelId);
			var ColorConvert= new ColorConverter();
			EmbedModel.Color = (Color)ColorConvert.ConvertFromString(w.Color);
			var temp = GodBot.SearchChannels.Channels();
			foreach (var tem in temp)
			{
				SelectListItem item = new SelectListItem()
				{
					Value = tem.ChannelId.ToString(),
					Text = tem.ChannelName.ToString(),
				};
				w.channelViewModels.Add(item);

			}
			w.channelViewModels.First().Selected = true;
			var sendEmbed = new GodBot.sendEmbed();
			var t = new GodBot.EmbedViewModel()
			{
				fildName1= w.fildName1,
				fildName2= w.fildName2,
				fildName3= w.fildName3,
				fildName4= w.fildName4,
				fildName5= w.fildName5,
				fildName6= w.fildName6,
				fildName7= w.fildName7,
				fildName8= w.fildName8,
				fildName9= w.fildName9,
				fildName10= w.fildName10,
				fildName11= w.fildName11,
				fildName12= w.fildName12,
				fildInline1 = w.fildInline1,
				fildInline2 = w.fildInline2,
				fildInline3 = w.fildInline3,
				fildInline10= w.fildInline10,
				fildInline11= w.fildInline11,
				fildInline12= w.fildInline12,
				fildInline4= w.fildInline4,
				fildInline5= w.fildInline5,
				fildInline6= w.fildInline6,
				fildInline7= w.fildInline7,
				fildInline8= w.fildInline8,
				fildInline9= w.fildInline9,
				fildValue1= w.fildValue1,
				fildValue2= w.fildValue2,
				fildValue3= w.fildValue3,
				fildValue4= w.fildValue4,
				fildValue5= w.fildValue5,
				fildValue6= w.fildValue6,
				fildValue7= w.fildValue7,
				fildValue8= w.fildValue8,
				fildValue10= w.fildValue10,
				fildValue11= w.fildValue11,
				fildValue12= w.fildValue12,
				fildValue9= w.fildValue9,
			};
			sendEmbed.Sand(t);
			return View(w);
		}
	}
}
