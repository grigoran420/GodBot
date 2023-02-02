using System;
using System.Threading;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Program
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!!!!");
            MainAsync();

            Thread.Sleep(-1);
        }


        static DiscordSocketClient client;
        static private DiscordSocketClient _client;

        public static async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "NzA0NzU4NTY3MDg0ODg0MDA4.GVM4iQ.qxZvwi0Y9ov7DCi2c_hjftMMcmNlUIfTWgLHZY";

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            client =  _client;
            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;

            _client.ModalSubmitted += async modal =>
            {
                // Get the values of components.
                List<SocketMessageComponentData> components =
                    modal.Data.Components.ToList();
                string food = components
                    .First(x => x.CustomId == "food_name").Value;
                string reason = components
                    .First(x => x.CustomId == "food_reason").Value;

                // Build the message to send.
                string message = "hey @everyone; I just learned " +
                    $"{modal.User.Mention}'s favorite food is " +
                    $"{food} because {reason}.";

                // Specify the AllowedMentions so we don't actually ping everyone.
                AllowedMentions mentions = new AllowedMentions();
                mentions.AllowedTypes = AllowedMentionTypes.Users;

                // Respond to the modal.
                await modal.RespondAsync(message, allowedMentions: mentions);
            };

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public static async Task Client_Ready()
        {
            // Let's build a guild command! We're going to need a guild so lets just put that in a variable.
            var guild = client.GetGuild(791600213424603146);

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
                await client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
                await client.CreateGlobalApplicationCommandAsync(RoleCommand.Build());
                await client.CreateGlobalApplicationCommandAsync(Food.Build());
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

            // Now, Let's respond with the embed.
            await command.RespondAsync(embed: embedBuiler.Build());
        }
        private static async Task SlashCommandHandler(SocketSlashCommand command)
        {
            //await command.RespondAsync($"You executed {command.Data.Name}");

            switch (command.Data.Name)
            {
                case "list-roles":
                    await HandleListRoleCommand(command);
                    break;
                case "член":
                    await FoodPreference(command);
                    break;
            }
        }

        [SlashCommand("food", "Tell us about your favorite food!")]
        public static async Task FoodPreference(SocketSlashCommand command)
        {
            var mb = new ModalBuilder()
            .WithTitle("Fav Food")
            .WithCustomId("food_menu")
            .AddTextInput("What??", "food_name", placeholder: "Pizza")
            .AddTextInput("Why??", "food_reason", TextInputStyle.Paragraph,
                "Kus it's so tasty");

            await command.RespondWithModalAsync(mb.Build());
        }
    }

}