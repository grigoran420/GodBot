using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using GodBot.Models;
using GodBot.Interfaces;

namespace GodBot
{
    public class NewMember : ISlashCommand
    {

        public static async Task VerificationHandler()
        {
            //var guild = Action._client.GetGuild(791600213424603146);
            //var guild = Action._client.GetGuild(805711346620432384);
            var verified = new SlashCommandBuilder();
            verified.WithName("verification").WithDescription("Начало верификации");
            try
            {
                await Action._client.CreateGlobalApplicationCommandAsync(verified.Build());
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
                case "verification":
                    await Verification(context);
                    break;
            }
        }
        public static async Task Verification(SocketSlashCommand context)
        {
            var modal = new ModalBuilder()
            {
                Title = "Игровая карточка участника",
                CustomId = "Page1",
            };
            modal.AddTextInput("Steam", "_steam", required: true)
            .AddTextInput("Epic Games", "_epic", required: false)
            .AddTextInput("Origin", "_origin", required: false)
            .AddTextInput("XBox", "_xBox", required: false)
            .AddTextInput("Genshin Impact", "_genshin", required: false);
            await context.RespondWithModalAsync(modal.Build());
        }

        public static async Task VerifiedModal(SocketModal modal)
        {
            switch (modal.Data.CustomId)
            {

                case "Page1":
                    {
                        List<SocketMessageComponentData> component = modal.Data.Components.ToList();
                        var user = new UserCard()
                        {
                            user = modal.User,
                            steam = component.First(x => x.CustomId == "_steam").Value,
                            epic = component.First(x => x.CustomId == "_epic").Value,
                            origin = component.First(x => x.CustomId == "_origin").Value,
                            xBox = component.First(x => x.CustomId == "_xBox").Value,
                            genshin = component.First(x => x.CustomId == "_genshin").Value,
                        };

                        if (user.steam.Replace(" ", "") == ""
                            & user.epic.Replace(" ", "") == ""
                            & user.origin.Replace(" ", "") == ""
                            & user.xBox.Replace(" ", "") == ""
                            & user.genshin.Replace(" ", "") == "")
                        {
                            var builderbuttonPage1 = new ComponentBuilder().WithButton("Изменить данные на странице 1", "Page1", row: 0);
                            await modal.RespondAsync(components: builderbuttonPage1.Build(), text: $"{modal.User.Mention} Вы не ввели данные. Нажмите на кнопку, чтоб ввести данные снова", ephemeral: true);
                            break;
                        }

                        userModel userData = new();
                        userData.Id = modal.User.Id;
                        userData.Name = modal.User.Username;
                        userData.userAutarUrl = modal.User.GetAvatarUrl();

                        var build = new EmbedBuilder();
                        //build.WithAuthor(Action._client.CurrentUser.Username, "https://phonoteka.org/uploads/posts/2021-07/1625657618_33-phonoteka-org-p-anime-art-sneg-krasivo-36.jpg");
                        build.WithTitle($"Ваша игравая карточка будет выглядеть вот так:\n Игровая карточка участника `@{modal.User.Username}:`");
                        build.WithThumbnailUrl(modal.User.GetAvatarUrl());

                        if (user.steam.Replace(" ", "") != "") { build.AddField("Steam", user.steam); 
                            userData.Steam = user.steam; } else userData.Steam = "";
                        if (user.epic.Replace(" ", "") != "") { build.AddField("Epic Games", user.epic); 
                            userData.Epic = user.epic; } else userData.Epic = "";
                        if (user.origin.Replace(" ", "") != "") { build.AddField("Origin", user.origin); 
                            userData.Origin = user.origin; } else userData.Origin = "";
                        if (user.xBox.Replace(" ", "") != "") { build.AddField("XBox", user.xBox); 
                            userData.Xbox = user.xBox; } else userData.Xbox = "";
                        if (user.genshin.Replace(" ", "") != "") { build.AddField("Genshin Impact", user.genshin); 
                            userData.Genshin = user.genshin; } else userData.Genshin = "";

                        try
                        {
                            using (var f = new StreamWriter("temp.json", false))
                            {
                                string s = JsonSerializer.Serialize(userData);
                                await f.WriteAsync(s);
                                Logger.LogToFile("System", $"Create or edit user card:\n {s}");
                                f.Close();
                            }
                        }
                        catch (Exception ex) 
                        {
                            using (var c = new FileStream("temp.json", FileMode.Create)) { c.Close(); };
                            using (var f = new StreamWriter("temp.json", false))
                            {
                                string s = JsonSerializer.Serialize(userData);
                                await f.WriteLineAsync(s);
                                Logger.LogToFile("Warring!", $"Warring! -> {ex.Message}\n Create or edit user card:\n {s}");
                                f.Close();
                            }
                        }

                        var builderbuttonPage2 = new ComponentBuilder()
                        .WithButton("Изменить данные на странице 1", "Page1", row: 0).WithButton("Следующий шаг", "Page2", row: 0);
                        await modal.RespondAsync(components: builderbuttonPage2.Build(), embed: build.Build(), ephemeral: true);
                    }
                    break;

                case "Page2":
                    {
                        List<SocketMessageComponentData> component = modal.Data.Components.ToList();
                        var user = new UserCard()
                        {
                            user = modal.User,
                            osu = component.First(x => x.CustomId == "_osu").Value,
                            social = component.First(x => x.CustomId == "_social").Value,
                            wargaming = component.First(x => x.CustomId == "_wargaming").Value,
                            ganres = component.First(x => x.CustomId == "_ganres").Value,
                            games = component.First(x => x.CustomId == "_games").Value,
                        };
                        if (user.osu.Replace(" ", "") == ""
                            & user.social.Replace(" ", "") == ""
                            & user.wargaming.Replace(" ", "") == ""
                            & user.ganres.Replace(" ", "") == ""
                            & user.games.Replace(" ", "") == "")
                        {
                            var builderbuttonPage1 = new ComponentBuilder().WithButton("Изменить данные на странице 1", "Page1", row: 0);
                            await modal.RespondAsync(components: builderbuttonPage1.Build(), text: $"{modal.User.Mention} Вы не ввели данные. Нажмите на кнопку, чтоб ввести данные снова", ephemeral: true);
                            break;
                        }
                        
                        userModel userData = new();

                        var build = new EmbedBuilder();
                        //build.WithAuthor(Action._client.CurrentUser.Username, "https://phonoteka.org/uploads/posts/2021-07/1625657618_33-phonoteka-org-p-anime-art-sneg-krasivo-36.jpg");
                        build.WithTitle($"Ваша игравая карточка будет выглядеть вот так:\n Игровая карточка участника `@{modal.User.Username}:`");
                        build.WithThumbnailUrl(modal.User.GetAvatarUrl());

                        if (user.osu.Replace(" ", "") != "") { build.AddField("Osu!", user.osu); 
                            userData.Osu = user.osu; } else userData.Osu = "";
                        if (user.social.Replace(" ", "") != "") { build.AddField("Social Club (Rockstar Games)", user.social); 
                            userData.Social = user.social; } else userData.Social = "";
                        if (user.wargaming.Replace(" ", "") != "") { build.AddField("Wargaming", user.wargaming); 
                            userData.Wargaming = user.wargaming; } else userData.Wargaming = "";
                        if (user.ganres.Replace(" ", "") != "") { build.AddField("Что нравится в играх/Жанры, которые интересуют", user.ganres); 
                            userData.ganres = user.ganres; } else userData.ganres = "";
                        if (user.games.Replace(" ", "") != "") { build.AddField("Любимые игры", user.games); 
                            userData.Games = user.games; } else userData.Games = "";
                        //userModel userData = new();
                        //if (user.osu.Replace(" ", "") != "") userData.Osu = user.osu; else userData.Osu = "";
                        //if (user.social.Replace(" ", "") != "") userData.Social = user.social; else userData.Social = "";
                        //if (user.wargaming.Replace(" ", "") != "") userData.Wargaming = user.wargaming; else userData.Wargaming = "";
                        //if (user.ganres.Replace(" ", "") != "") userData.ganres = user.ganres; else userData.ganres = "";
                        //if (user.games.Replace(" ", "") != "") userData.Games = user.games; else userData.Games = "";

                        try
                        {
                            userModel? tempUser = new();
                            using (var r = new StreamReader("temp.json"))
                            {
                                string temp = r.ReadToEnd();
                                tempUser = JsonSerializer.Deserialize<userModel>(temp);
                                r.Close();
                            }
                            userData.Id = tempUser.Id; 
                            userData.Name = tempUser.Name;
                            userData.userAutarUrl = tempUser.userAutarUrl;
                            userData.Steam = tempUser.Steam;
                            userData.Epic = tempUser.Epic;
                            userData.Origin = tempUser.Origin;
                            userData.Xbox = tempUser.Xbox;
                            userData.Genshin = tempUser.Genshin;
                            using (var f = new StreamWriter("temp.json", false))
                            {
                                string s = JsonSerializer.Serialize(userData);
                                await f.WriteAsync(s);
                                Logger.LogToFile("System", $"Updated user card:\n {s}");
                                f.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            using (var c = new FileStream("temp.json", FileMode.Create)) { c.Close(); };
                            using (var f = new StreamWriter("temp.json", false))
                            {
                                string s = JsonSerializer.Serialize(userData);
                                await f.WriteLineAsync(s);
                                Logger.LogToFile("Warring!", $"Warring! -> {ex.Message}\n Updated user card:\n {s}");
                                f.Close();
                            }
                        }

                        var builderbuttonPage2 = new ComponentBuilder()
                        .WithButton("Изменить данные на странице 2", "Page2", row: 0).WithButton("Отправить форму", "Complite", row: 0);
                        await modal.RespondAsync(components: builderbuttonPage2.Build(), embed: build.Build(), ephemeral: true);
                    }
                    break;
            }
        }

        public static async Task VerificationButton(SocketMessageComponent component)
        {
            switch (component.Data.CustomId)
            {
                // Since we set our buttons custom id as 'custom-id', we can check for it like this:
                case "Page1":
                    var modal1 = new ModalBuilder()
                    {
                        Title = "Игровая карточка участника",
                        CustomId = "Page1",
                    };
                    modal1.AddTextInput("Steam", "_steam", required: true);
                    modal1.AddTextInput("Epic Games", "_epic", required: false);
                    modal1.AddTextInput("Origin", "_origin", required: false);
                    modal1.AddTextInput("XBox", "_xBox", required: false);
                    modal1.AddTextInput("Genshin Impact", "_genshin", required: false);
                    await component.RespondWithModalAsync(modal1.Build());
                    break;
                case "Page2":
                    var modal2 = new ModalBuilder()
                    {
                        Title = "Игровая карточка участника",
                        CustomId = "Page2",
                    };
                    modal2.AddTextInput("Osu!", "_osu", required: false);
                    modal2.AddTextInput("Social Club (Rockstar Games)", "_social", required: false);
                    modal2.AddTextInput("Wargaming", "_wargaming", required: false);
                    modal2.AddTextInput("Жанры, которые интересуют", "_ganres", TextInputStyle.Paragraph, required: true);
                    modal2.AddTextInput("Любимые игры", "_games", TextInputStyle.Paragraph, required: false);
                    await component.RespondWithModalAsync(modal2.Build());
                    break;
                case "Complite":
                    userModel? tempUser = new();
                    using (var r = new StreamReader("temp.json"))
                    {
                        string temp = r.ReadToEnd();
                        tempUser = JsonSerializer.Deserialize<userModel>(temp);
                        r.Close();
                    }
                    tempUser.Complete = true;
                    tempUser.verified = false;
                    try
                    {
                        using (var c = new FileStream($"users/{tempUser.Id}.json", FileMode.Create)) { c.Close(); };
                        using (var f = new StreamWriter($"users/{tempUser.Id}.json", false))
                        {
                            string s = JsonSerializer.Serialize(tempUser);
                            await f.WriteAsync(s);
                            Logger.LogToFile("System", $"Complete user card:\n Username:{tempUser.Name} discord id: {tempUser.Id}");
                            f.Close();
                        }
                        await component.RespondAsync($"{component.User.Mention}, Ваша заяка успешно принята. Ожидайте одобрения от модерации.", ephemeral: true);
                    }
                    catch(Exception ex) 
                    {
                        using (var c = new FileStream($"users/{tempUser.Id}.json", FileMode.Create)) { c.Close(); };
                        using (var f = new StreamWriter($"users/{tempUser.Id}.json", false))
                        {
                            string s = JsonSerializer.Serialize(tempUser);
                            await f.WriteLineAsync(s);
                            Logger.LogToFile("Warring!", $"Warring! -> {ex.Message}\n Complete user card:\n Username:{tempUser.Name} discord id: {tempUser.Id}");
                            f.Close();
                        }
                        await component.RespondAsync($"{component.User.Mention}, Ваша заяка успешно принята. Ожидайте одобрения от модерации.", ephemeral: true);
                    }
                    break;
                }
        }
    }
}
