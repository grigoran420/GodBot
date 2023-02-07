using System;
using System.Threading;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace GodBot
{
    public class Action
    {
		static internal DiscordSocketClient _client;
		public static async void Start()
        {
			_client = new DiscordSocketClient();

			_client.Log += Logger.Log;
			CommandService command = new CommandService();
			command.Log += Logger.Log;

			//  You can assign your bot token to a string, and pass that in to connect.
			//  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
			//var token = "token";//токе бота 
            var token = "token";//токе бота 

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();
            _client.ButtonExecuted += ButtonHandler;
            _client.ButtonExecuted += NewMember.VerificationButton;
            //_client.Ready += Client_Ready;
			_client.Ready += NewMember.VerificationHandler;
            //_client.SlashCommandExecuted += SlashCommandHandler;
            _client.SlashCommandExecuted += NewMember.SlashCommandHandler;
			_client.ModalSubmitted += NewMember.VerifiedModal;
            /*var builderbutton = new ComponentBuilder()
            .WithButton("пидр", "verification");
            var button = builderbutton.Build();
			await _client.GetGuild(791600213424603146).GetTextChannel(1041677932873134091).SendMessageAsync(text: "Хей пидр гнойный пора пояснить браткам хто ты", components: button);*/
        }
        public static async Task ButtonHandler(SocketMessageComponent component)
        {
            // We can now check for our custom id
            switch (component.Data.CustomId)
            {
                // Since we set our buttons custom id as 'custom-id', we can check for it like this:
                case "custom-id":
                    // Lets respond by sending a message saying they clicked the button
                    await component.RespondAsync($"{component.User.Mention} has clicked the button!");
                    break;
            }
        }
        public static async void Reboot()
		{
			await _client.StopAsync();
			await _client.LogoutAsync();
			Thread.Sleep(10000);
			Start();
		}
        public static async void Shutdown()
		{
            await _client.StopAsync();
            await _client.LogoutAsync();
        }
        #region
        public static async Task Client_Ready()
		{
			// Let's build a guild command! We're going to need a guild so lets just put that in a variable.
			var guild = _client.GetGuild(791600213424603146);

			// Next, lets create our slash command builder. This is like the embed builder but for slash commands.
			var guildCommand = new SlashCommandBuilder();

			// Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$
			guildCommand.WithName("first-command");

			// Descriptions can have a max length of 100.
			guildCommand.WithDescription("This is my first guild slash command!");

			// Let's do our global command
			var globalCommand = new SlashCommandBuilder();
			globalCommand.WithName("first-global-command");
			globalCommand.WithDescription("This is my first global slash command");
            var RoleCommand = new Discord.SlashCommandBuilder()
				.WithName("list-roles")
				.WithDescription("Lists all roles of a user.")
				.AddOption("user", ApplicationCommandOptionType.User, "The users whos roles you want to be listed", isRequired: true);

			var Food = new Discord.SlashCommandBuilder().WithName("член").WithDescription("write food")/*.WithDefaultMemberPermissions(GuildPermission.)*/;

			try
			{
				// Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
				await guild.CreateApplicationCommandAsync(guildCommand.Build());

				// With global commands we don't need the guild.
				await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
				await _client.CreateGlobalApplicationCommandAsync(RoleCommand.Build());
				await _client.CreateGlobalApplicationCommandAsync(Food.Build());
				// Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
				// For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
			}
			catch (ApplicationCommandException exception)
			{
				// If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
				var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

				// You can send this error somewhere or just print it to the console, for this example we're just going to print it.
				Console.WriteLine(json);
			}
		}

		private static async Task HandleListRoleCommand(SocketSlashCommand command)
		{
			// We need to extract the user parameter from the command. since we only have one option and it's required, we can just use the first option.
			var guildUser = (SocketGuildUser)command.Data.Options.First().Value;

			// We remove the everyone role and select the mention of each role.
			var roleList = string.Join(",\n", guildUser.Roles.Where(x => !x.IsEveryone).Select(x => x.Mention));

			var embedBuiler = new EmbedBuilder()
				.WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
				.WithTitle("Roles")
				.WithDescription(roleList)
				.WithColor(Color.Green)
				.WithCurrentTimestamp();
			var builderbutton = new ComponentBuilder()
			.WithButton("label", "custom-id");
			var button = builderbutton.Build();
            // Now, Let's respond with the embed.
            await command.RespondAsync(embed: embedBuiler.Build(), components: button);
		}
		private static async Task SlashCommandHandler(SocketSlashCommand command)
		{
			//await command.RespondAsync($"You executed {command.Data.Name}");

			switch (command.Data.Name)
			{
				case "list-roles":
                    await HandleListRoleCommand(command);
					break;
			}
		}
		#endregion
	}
}