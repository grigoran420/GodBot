﻿using Discord;
using Discord.WebSocket;
using GodBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GodBot.Controllers
{
    public class SendMessage
    {
        public async void sendAccept(userModel user)
        {
            var embed = new EmbedBuilder()
            {
                Title = $"**Игровая карточка участника:`{user.Name}`**",
                ThumbnailUrl = user.userAutarUrl,
                Author = new EmbedAuthorBuilder() { Name = "[CGP]Central Gaming Public", IconUrl = "https://images-ext-2.discordapp.net/external/fBlpeO4VuoXd0FvCzB4Nt9WIBjlIZrT23m76BT8lpAw/https/images-ext-1.discordapp.net/external/nqHcVm1UMhJ_CZDy7QVQphVPhYSpgrcuQBmF5kw1zVM/https/cdn.discordapp.com/icons/805711346620432384/e18da31784eb04c70128494a146b5dfe.png" }
            };
            if (user.Steam != "") embed.AddField("**Steam:**", $"`{user.Steam}`");
            if (user.Epic != "") embed.AddField("**EpicGames:**", $"`{user.Epic}`", true);
            if (user.Origin != "") embed.AddField("**Origin:**", $"`{user.Origin}`", true);
            if (user.Xbox != "") embed.AddField("**XBox:**", $"`{user.Xbox}`");
            if (user.Genshin != "") embed.AddField("**Genshin Impact:**", $"`{user.Genshin}`");
            if (user.Osu != "") embed.AddField("**Osu!:**", $"`{user.Osu}`");
            if (user.Social != "") embed.AddField("**Social Club (Rockstar Games):**", $"`{user.Social}`");
            if (user.Wargaming != "") embed.AddField("**Wargaming:**", $"`{user.Wargaming}`");
            if (user.ganres != "") embed.AddField("**Что нравится в играх/Жанры, которые интересуют:**", $"`{user.ganres}`");
            if (user.Games != "") embed.AddField("**Любимые игры:**", $"`{user.Games}`");
            //await Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(1041677932873134091).SendMessageAsync(text: $"<@!{user.Id}>",embed: embed.Build());
            await Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(913817793307222076).SendMessageAsync(text: $"<@!{user.Id}>",embed: embed.Build());
        }
        public async void sendReject(userModel user)
        {
            /*SocketUser user1 = Action._client.GetGuild(805711346620432384 id гильдиии куда отправляется сообщение).GetUser(491487830862987266);
            Console.WriteLine("efsd"); */ 
            await Action._client.GetGuild(805711346620432384 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(913817793307222076).SendMessageAsync($"<@!{user.Id}> Ваша зявка отклонена, пройдите верификацию заново. ");
            //await Action._client.GetGuild(791600213424603146 /*id гильдиии куда отправляется сообщение*/).GetTextChannel(1041677932873134091).SendMessageAsync($"<@!{user.Id}> Ваша зявка отклонена, пройдите верификацию заново. ");
        }
    }
}
