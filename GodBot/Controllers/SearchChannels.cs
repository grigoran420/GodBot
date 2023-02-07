using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBot
{
    public class SearchChannels: Action
    {
        public static List<ChannelViewModel> Channels()
        {
            //var guild = _client.GetGuild(791600213424603146 /*id гильдиии где читаются все каналы*/);
            var guild = _client.GetGuild(805711346620432384 /*id гильдиии где читаются все каналы*/);
            List<ChannelViewModel> temp = new List<ChannelViewModel>();
            var Channels = guild.TextChannels.ToList();
            foreach (var Channel in Channels)
            {
                ChannelViewModel tempModel = new ChannelViewModel();
                tempModel.ChannelName = Channel.Name;
                tempModel.ChannelId = Channel.Id;
                temp.Add(tempModel);
            }
            return temp;
            
        }
    }
}
