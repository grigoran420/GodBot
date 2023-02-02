using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GodBot
{
    public class NewMember
    {

        public static async Task VerificationHandler()
        {
            var guild = Action._client.GetGuild(791600213424603146);
            var verified = new SlashCommandBuilder();
            verified.WithName("verification").WithDescription("Начало верификации");
            try
            {
                await guild.CreateApplicationCommandAsync(verified.Build());
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
            modal.AddTextInput("Steam", "_steam")
            .AddTextInput("Epic Games", "_epic")
            .AddTextInput("Origin", "_origin")
            .AddTextInput("XBox", "_xBox")
            .AddTextInput("Genshin Impact", "_genshin");
            await context.RespondWithModalAsync(modal.Build());
            /*modal.AddTextInput("Osu!", "_osu");
            modal.AddTextInput("Social Club (Rockstar Games)", "_social");
            modal.AddTextInput("Wargaming", "_wargaming");
            modal.AddTextInput("Что нравится в играх/Жанры, которые интересуют", "_ganres", TextInputStyle.Paragraph);
            modal.AddTextInput("Любимые игры", "_games", TextInputStyle.Paragraph);
            await context.RespondWithModalAsync(modal.Build());*/
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
                            /* = components.First(x => x.CustomId == "_osu").Value,
                            social = components.First(x => x.CustomId == "_social").Value,
                            wargaming = components.First(x => x.CustomId == "_wargaming").Value,
                            ganres = components.First(x => x.CustomId == "_ganres").Value,
                            games = components.First(x => x.CustomId == "_games").Value,*/
                        };

                        var build = new EmbedBuilder();
                        //build.WithAuthor(Action._client.CurrentUser.Username, "https://phonoteka.org/uploads/posts/2021-07/1625657618_33-phonoteka-org-p-anime-art-sneg-krasivo-36.jpg");
                        build.WithTitle($"Ваша игравая карточка будет выглядеть вот так:\n Игровая карточка участника `@{modal.User.Username}:`");
                        build.WithThumbnailUrl(modal.User.GetAvatarUrl());
                        if (user.steam != null) { build.AddField("Steam", user.steam); }
                        if (user.epic != null) { build.AddField("Epic Games", user.epic); }
                        if (user.origin != null) { build.AddField("Origin", user.origin); }
                        if (user.xBox != null) { build.AddField("XBox", user.xBox); }
                        if (user.genshin != null) { build.AddField("Genshin Impact", user.genshin); }
                        var builderbuttonPage2 = new ComponentBuilder()
                        .WithButton("Изменить данные на странице 1", "Page1", row: 0).WithButton("Следующий шаг", "Page2", row: 0);
                        var builderbuttonPage1 = new ComponentBuilder().WithButton("Edit page 1", "Page1");
                        /*if (user.osu != null) { build.AddField("Osu!", user.osu); }
                        if (user.social != null) { build.AddField("Social Club (Rockstar Games)", user.social); }
                        if (user.wargaming != null) { build.AddField("Wargaming", user.wargaming); }
                        if (user.ganres != null) { build.AddField("Что нравится в играх/Жанры, которые интересуют", user.ganres); }
                        if (user.games != null) { build.AddField("Любимые игры", user.games); }*/

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

                        var build = new EmbedBuilder();
                        //build.WithAuthor(Action._client.CurrentUser.Username, "https://phonoteka.org/uploads/posts/2021-07/1625657618_33-phonoteka-org-p-anime-art-sneg-krasivo-36.jpg");
                        build.WithTitle($"Ваша игравая карточка будет выглядеть вот так:\n Игровая карточка участника `@{modal.User.Username}:`");
                        build.WithThumbnailUrl(modal.User.GetAvatarUrl());
                        var builderbuttonPage2 = new ComponentBuilder()
                        .WithButton("Изменить данные на странице 2", "Page2", row: 0).WithButton("Отправить форму", "Complite", row: 0);
                        if (user.osu != null) { build.AddField("Osu!", user.osu); }
                        if (user.social != null) { build.AddField("Social Club (Rockstar Games)", user.social); }
                        if (user.wargaming != null) { build.AddField("Wargaming", user.wargaming); }
                        if (user.ganres != null) { build.AddField("Что нравится в играх/Жанры, которые интересуют", user.ganres); }
                        if (user.games != null) { build.AddField("Любимые игры", user.games); }

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
                    modal1.AddTextInput("Steam", "_steam");
                    modal1.AddTextInput("Epic Games", "_epic");
                    modal1.AddTextInput("Origin", "_origin");
                    modal1.AddTextInput("XBox", "_xBox");
                    modal1.AddTextInput("Genshin Impact", "_genshin");
                    /*modal.AddTextInput("Osu!", "_osu");
                    modal.AddTextInput("Social Club (Rockstar Games)", "_social");
                    modal.AddTextInput("Wargaming", "_wargaming");
                    modal.AddTextInput("Что нравится в играх/Жанры, которые интересуют", "_ganres", TextInputStyle.Paragraph);
                    modal.AddTextInput("Любимые игры", "_games", TextInputStyle.Paragraph);*/
                    await component.RespondWithModalAsync(modal1.Build());
                    break;
                case "Page2":
                    var modal2 = new ModalBuilder()
                    {
                        Title = "Игровая карточка участника",
                        CustomId = "Page2",
                    };
                    modal2.AddTextInput("Osu!", "_osu");
                    modal2.AddTextInput("Social Club (Rockstar Games)", "_social");
                    modal2.AddTextInput("Wargaming", "_wargaming");
                    modal2.AddTextInput("Жанры, которые интересуют", "_ganres", TextInputStyle.Paragraph);
                    modal2.AddTextInput("Любимые игры", "_games", TextInputStyle.Paragraph);
                    await component.RespondWithModalAsync(modal2.Build());
                    break;
                }
        }
    }
}
