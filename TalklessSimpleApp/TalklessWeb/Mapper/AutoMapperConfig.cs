using AutoMapper;
using TalklessData.Entities;
using TalklessWeb.Models;

namespace AutomapperConfig
{
    public class AutoMapperConfig
    {

        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAccount, ProfileViewModel>();
                cfg.CreateMap<ProfileViewModel, UserAccount>();
                cfg.CreateMap<Message, MessageViewModel>();
            });

            return config;
        }
    }
}