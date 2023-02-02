using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBot
{
    public class UserCard
    {
        public SocketUser user { get; set; }
        public string? steam { get; set; } = null;
        public string? epic { get; set; } = null;
        public string? origin { get; set; } = null;
        public string? xBox { get; set; } = null;
        public string? genshin { get; set; } = null;
        public string? osu { get; set; } = null;
        public string? social { get; set; } = null;
        public string? wargaming { get; set; } = null;
        public string? ganres { get; set; } = null;
        public string? games { get; set; } = null;
    }
}
