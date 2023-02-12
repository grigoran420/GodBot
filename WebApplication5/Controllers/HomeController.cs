using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication5.Models;
using GodBot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmbedViewModel = WebApplication5.Models.EmbedViewModel;
using UserViewModel = WebApplication5.Models.userViewModel;
using UserModel = GodBot.Models.userModel;
using GodBot.Models;
using GodBot.Controllers;
using System.Text.Json;
using System;
using static System.Collections.Specialized.BitVector32;
using Microsoft.VisualBasic;
using Discord;
using System.Runtime.CompilerServices;

namespace WebApplication5.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #region
        public IActionResult Index()
		{
            //MixedModel model = new MixedModel();
            GodBot.Logger.LogToFile("System", "Bot is start");
            string[] files = Directory.GetFiles("Embeds", "*.json");

			Models.EmbedViewModel Model = new();
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

					Model.sendedEmbed.Add(item);
                }
            }
            if (Model.sendedEmbed.Count != 0)
			    Model.sendedEmbed.First().Selected = true;

			var temp = GodBot.SearchChannels.Channels();
            foreach (var t in temp)
            {
                SelectListItem item = new SelectListItem() 
                {
                    Value=t.ChannelId.ToString(),
                    Text=t.ChannelName.ToString(),
                };
				Model.channelViewModels.Add(item);

			}
			if (Model.channelViewModels.Count != 0)
				Model.channelViewModels.First().Selected = true;
			var ColorConverter = new ColorConverter();
            var Color = ColorConverter.ConvertFromString("#DEB487");

			return View(Model);
		}
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Tickets()
        {

            GodBot.Logger.LogToFile("Info", "Go to page \"Tickets\"");
            string[] files = Directory.GetFiles("users", "*.json");
            UserModel[] users = new UserModel[files.Length];
            foreach (string file in files)
                GodBot.Logger.LogToFile("Info", $"Detected file{file}");
            for (int i = 0; i < files.Length; i++)
            {
                UserModel? tempUser = new();
                using (var r = new StreamReader(files[i]))
                {
                    string temp = r.ReadToEnd();
                    tempUser = JsonSerializer.Deserialize<UserModel>(temp);
                    r.Close();
                }
                if (tempUser.verified == false & tempUser.Complete == true)
                    users[i] = tempUser;
            }
            List<UserModel> newUser = new List<UserModel>();
            foreach (var user in users)
            {
                if (user != null) { newUser.Add(user); } 
            }
            return View(newUser);
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Logs()
        {
			List<string> logs = new List<string>();
			try
			{
				using (var fs = new StreamReader("temp.log"))
				{
					while (!fs.EndOfStream)
					{
						logs.Add(fs.ReadLine());
					}
				};
			}
			catch(Exception ex)
			{
				logs.Add(ex.ToString());
				using (var fs = System.IO.File.Create("temp.log")) ;
					
			}
            return View(logs);
        }

        #endregion
        [HttpPost]
		public ActionResult Tickets (string action)
		{
            GodBot.Logger.LogToFile("Info", "Go to page \"Tickets\"");
            string[] files = Directory.GetFiles("users", "*.json");
            UserModel[] users = new UserModel[files.Length];
            foreach (string file in files)
                GodBot.Logger.LogToFile("Info", $"Detected file{file}");
            for (int i = 0; i < files.Length; i++)
            {
                UserModel? tempUser = new();
                using (var r = new StreamReader(files[i]))
                {
                    string temp = r.ReadToEnd();
                    tempUser = JsonSerializer.Deserialize<UserModel>(temp);
                    r.Close();
                }
                if (action == $"accept{{{tempUser.Id}}}")
                {
                    tempUser.verified = true;
                    SendMessage message = new();
                    message.sendAccept(tempUser);
                    using (var f = new StreamWriter($"users/{tempUser.Id}.json", false))
                    {
                        string s = JsonSerializer.Serialize(tempUser);
                        f.WriteLine(s);
                        Logger.LogToFile("Success!", $" Success user card:\n {tempUser.Name}");
                        f.Close();
                    }
                }else if (action == $"reject{{{tempUser.Id}}}")
                {
                    tempUser.Complete = false;
                    using (var f = new StreamWriter($"users/{tempUser.Id}.json", false))
                    {
                        string s = JsonSerializer.Serialize(tempUser);
                        f.WriteLine(s);
                        Logger.LogToFile("Reject!", $" User card Removed:\n {tempUser.Name}");
                        f.Close();
                    }
                    SendMessage message = new();
                    message.sendReject(tempUser);
                }
                if (tempUser.verified == false & tempUser.Complete == true) 
                users[i] = tempUser;
            }

            ViewData["action"] = action;
            List<UserModel> newUser = new List<UserModel>();
            foreach (var user in users)
            {
                if (user != null) { newUser.Add(user); }
            }
            return View(newUser);
        }
        [HttpPost]
        public ActionResult Logs(string action)
        {
            GodBot.Logger.LogToFile("Info", "Go to page \"Logs\"");
            ViewData["action"] = action;
            List<string> logs = new List<string>();
            try
            {
                using (var fs = new StreamReader("temp.log"))
                {
                    while (!fs.EndOfStream)
                    {
                        logs.Add(fs.ReadLine());
                    }
                };
            }
            catch (Exception ex)
            {
                logs.Add(ex.ToString());
                using (var fs = System.IO.File.Create("temp.log")) ;

            }
            return View(logs);
        }
        [HttpPost]
        public ActionResult Settings(string action)
        {
            GodBot.Logger.LogToFile("Info", "Go to page \"Settings\"");
            ViewData["action"] = action;
            return View();
        }
        [HttpPost]
		public ActionResult Index(string action, EmbedViewModel? w = null)
		{
			if (action == "Embed")
			{
				GodBot.Logger.LogToFile("Info", "Go to page \"Embed\"");
				ViewBag.Message = action;
			}
			else if (action == "Start")
			{
                
				GodBot.Logger.LogToFile("System", "Bot is start");
				GodBot.Action.Start();
                return RedirectToAction("Logs");
            }
			else if (action == "Reboot")
			{
				GodBot.Logger.LogToFile("System", "Bot is reboot");
				GodBot.Action.Reboot();
                return RedirectToAction("Logs");
            }else if(action == "Shutdown")
            {
                GodBot.Logger.LogToFile("System", "Bot is shutdown");
                GodBot.Action.Shutdown();
                return RedirectToAction("Logs");
            }

			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}