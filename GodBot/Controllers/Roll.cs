using Discord;
using Discord.WebSocket;
using GodBot.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GodBot.Controllers
{
	internal class Roll : ISlashCommand
	{
		public static async Task SlashCommand()
		{
			var roll = new SlashCommandBuilder();
			ApplicationCommandOptionChoiceProperties[] subCommand = new ApplicationCommandOptionChoiceProperties[1] 
				{ new ApplicationCommandOptionChoiceProperties{ Name = "Tarkov", Value = "Tarkov" } };

			
			roll.WithName("roll")
				.WithDescription("Зароль сэтап")
				.AddOption("game", ApplicationCommandOptionType.String, "Выбери игру, в которой нужно заролить сэтап", choices: subCommand, isRequired: true)
				.AddOption("params", ApplicationCommandOptionType.String, "Введи парамерты сэтапа");
			try
			{
				await Action._client.CreateGlobalApplicationCommandAsync(roll.Build());
			}
			catch (Exception ex)
			{

				Logger.LogToFile("System", ex.Message);
			}
		}

		public static async Task SlashCommandHandler(SocketSlashCommand context)
		{
			switch (context.Data.Name) 
			{
				case "roll":
					string param = "";
					var Options = context.Data.Options.ToList();

					foreach (var option in Options) 
					{
						if (option.Name == "params") param = option.Value.ToString();
					}

					foreach (var option in Options)
					{
						switch (option.Value)
						{
							case "Tarkov":
								RollTarkov(context, param);
								break;
						}
					}

					break;
			}
		}
		private static async void RollTarkov(SocketSlashCommand ctx, string param)
		{
			Logger.LogToFile("System", $"User ({ctx.User.Username}) used command Roll with params: {{Game: \"Tarkov\"}}, params: \"{param}\")}}");
			string Gun, Map, Armor, Helmet, Medical1, Medical2, Backpack = "";
			Models.TarkovModel tarkov = new Models.TarkovModel();
			Random random = new Random();
			Gun = tarkov.Gun[random.Next(tarkov.Gun.Count - 1)];
			Map = tarkov.Map[random.Next(tarkov.Map.Count - 1)];
			Armor = tarkov.Armor[random.Next(tarkov.Armor.Count - 1)];
			Helmet = tarkov.Helmet[random.Next(tarkov.Helmet.Count - 1)];
			Medical1 = tarkov.Medical[random.Next(tarkov.Medical.Count - 1)];
			Medical2 = tarkov.Medical[random.Next(tarkov.Medical.Count - 1)];
			Backpack = tarkov.Backpack[random.Next(tarkov.Backpack.Count - 1)];
			string[] target = 
			{ 
				"идет убивать диких на локацию", 
				"идет убивать ЧВК на локацию", 
				"идет лутать помойки на локацию", 
				"идет умирать на локацию", 
				"идет пылесосить локацию" 
			};
			string prefix = "";
			switch (Map)
			{
				case "Резерв":
					string[] targetReserv = 
					{ 
						"идет чистить бунекер на локицию", 
						"идет фармить босса на локицию", 
						"идет фармить рейдеров на локицию", 
						"идет за модулями на локицию", 
						"идет за патронами на локицию"
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetReserv[random.Next(targetReserv.Count() - 1)];
					break;
				case "Лабаратория":
					string[] targetLabs =
					{
						"идет умирать от читеров на локицию",
						"идет фармить фломастеры на локицию",
						"идет фармить Ledx на локицию",
						"идет фармить рейдеров на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetLabs[random.Next(targetLabs.Count() - 1)];
					break;
				case "Развязка":
					string[] targetUltra =
					{
						"идет искать колбасу на локицию",
						"идет за едой на локицию",
						"идет умирать от Кирилла на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetUltra[random.Next(targetUltra.Count() - 1)];
					break;
				case "Берег":
					string[] targetBeach =
					{
						"идет в санаторий на локицию",
						"идет искать санитара на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetBeach[random.Next(targetBeach.Count() - 1)];
					break;
				case "Улицы Таркова":
					prefix = target[random.Next(target.Count() - 1)];
					break;
				case "Завод":
					string[] targetCustom =
					{
						"идет искать тагиллу на локицию",
						"идет на мужское ПВП на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetCustom[random.Next(targetCustom.Count() - 1)];
					break;
				case "Маяк":
					string[] targetLight =
					{
						"идет фармить отступников на локицию",
						"идет за драгоценностями на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetLight[random.Next(targetLight.Count() - 1)];
					break;
				case "Таможня":
					string[] targetTamojna =
					{
						"идет в общаги на локицию",
						"идет за решалой на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetTamojna[random.Next(targetTamojna.Count() - 1)];
					break;
				case "Лес":
					string[] targetLes =
					{
						"идет за штурманом на локицию",
						"идет гулять на локицию",
					};
					if (random.Next(1) == 0) prefix = target[random.Next(target.Count() - 1)]; else prefix = targetLes[random.Next(targetLes.Count() - 1)];
					break;
			}
			await ctx.RespondAsync($"Игрок {ctx.User.Mention} {prefix} ||{Map}||, \nодевшись в ||{Armor}|| & ||{Helmet}|| & ||{Backpack}||, \nвзяв в руки ||{Gun}||, \nа личиться он будет ||{Medical1}|| & ||{Medical2}||. Желаем ему удачи");
		} 

	}
}
