using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication5.Models
{
    public class userViewModel
    {
        [JsonInclude]
        public ulong Id { get; set; }
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public string userAutarUrl { get; set; }
        [JsonInclude]
        public string? Steam { get; set; }
        [JsonInclude]
        public string? Epic { get; set; }
        [JsonInclude]
        public string? Origin { get; set; }
        [JsonInclude]
        public string? Xbox { get; set; }
        [JsonInclude]
        public string? Genshin { get; set; }
        [JsonInclude]
        public string? Osu { get; set; }
        [JsonInclude]
        public string? Social { get; set; }
        [JsonInclude]
        public string? Wargaming { get; set; } 
        [JsonInclude]
        public string? ganres { get; set; }
        [JsonInclude]
        public string? Games { get; set; }
        [JsonInclude]
        public bool Complete { get; set; } = false;
        [JsonInclude]
        public bool verified { get; set;} = false;
    }
}
