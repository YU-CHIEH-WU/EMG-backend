using AutoMapper;
using EMG.Model;
using EMG.Model.ViewModel;
using EMG.Model.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<SpResultMaoToEntityProfile>();
            });
        }
    }

    public class SpResultMaoToEntityProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "SpResultMaoToEntityProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<RegisterView, UserHabit>();
            Mapper.CreateMap<RegisterView, User>();
            Mapper.CreateMap<User, RegisterView>();
            Mapper.CreateMap<UserHabit, RegisterView>();
            Mapper.CreateMap<LoginView, User>();
            Mapper.CreateMap<User, ProfileView>();
            Mapper.CreateMap<ProfileView, User>();
            Mapper.CreateMap<Message, MessageView>();
            Mapper.CreateMap<List<MessageView>, List<Message>>();
            Mapper.CreateMap<MessageCreateView, Message>();
            Mapper.CreateMap<MessageView, Message>();
            Mapper.CreateMap<Reply, ReplyView>();
            Mapper.CreateMap<List<ReplyView>, List<Reply>>();
            Mapper.CreateMap<ReplyCreatView, Reply>();
            Mapper.CreateMap<ReplyView, Reply>();
            Mapper.CreateMap<File, FileView>();
            Mapper.CreateMap<List<FileView>, List<File>>();
            Mapper.CreateMap<Register2View, Usertest>();
            Mapper.CreateMap<LoginView, Usertest>();
            Mapper.CreateMap<List<EMGDataView>, List<EMGData>>();
            Mapper.CreateMap<EMGDataView, EMGData>();
            Mapper.CreateMap<List<IEMGView>, List<IEMGData>>();
            Mapper.CreateMap<IEMGView, IEMGData>();
            Mapper.CreateMap<List<RMSView>, List<RMSData>>();
            Mapper.CreateMap<RMSView, RMSData>();
            Mapper.CreateMap<News, NewsView>();
            Mapper.CreateMap<NewsView, News>();
            Mapper.CreateMap<List<NewsView>, List<News>>();
        }
    }
}