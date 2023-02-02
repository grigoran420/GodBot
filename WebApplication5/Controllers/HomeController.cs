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
            var Model = new Models.EmbedViewModel();
            var temp = GodBot.SearchChannels.Channels();
            foreach (var t in temp)
            {
                SelectListItem item = new SelectListItem() {
                Value=t.ChannelId.ToString(),
                Text=t.ChannelName.ToString(),
                };
				Model.channelViewModels.Add(item);

			}
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
            return View();
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
            ViewData["action"] = action;
            return View();
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