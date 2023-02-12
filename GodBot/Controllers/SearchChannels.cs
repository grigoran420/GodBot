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
            var guild = _client.GetGuild(805711346620432384 /*id гильдиии где читаются все каналы*/);
            //var guild = _client.GetGuild(805711346620432384 /*id гильдиии где читаются все каналы*/);
            List<ChannelViewModel> temp = new List<ChannelViewModel>();
            var Channels = guild.Channels.ToList();
            foreach (var Channel in Channels)
            {
                if (Channel.GetChannelType().ToString() == "Text" || Channel.GetChannelType().ToString() == "News")
                {
                    ChannelViewModel tempModel = new ChannelViewModel();
                    tempModel.ChannelName = Channel.Name;
                    tempModel.ChannelId = Channel.Id;
                    temp.Add(tempModel);
                }
            }
            return temp;
            
        }
    }
}
