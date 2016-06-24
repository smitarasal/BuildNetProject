using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QSI.Domain;
using QSI.Services.Spec;

namespace QSI.Services
{
   public class AutoMapBootStrap
    {

       public static void InitializeMap()
       {

           Mapper.CreateMap<User, UsersDto>()
               .ForMember(dest => dest.UserPreference, src => src.Ignore())
               .ForMember(dest => dest.UserData, src => src.Ignore())
              .ForMember(dest => dest.ShareWidgets, src => src.Ignore());
           Mapper.CreateMap<UsersDto, User>();
           Mapper.CreateMap<UserGroup, UserGroupDto>();
           Mapper.CreateMap<UserGroupDto, UserGroup>();
           Mapper.CreateMap<Client, ClientDto>();
           Mapper.CreateMap<ClientDto, Client>();
           Mapper.CreateMap<UserPreference, UserPreferenceDto>();
           Mapper.CreateMap<UserPreferenceDto, UserPreference>();
           Mapper.CreateMap<UserData, UserDataDto>();
           Mapper.CreateMap<UserDataDto, UserData>();
           Mapper.CreateMap<UserLogging, UserLoggingDto>();
           Mapper.CreateMap<UserLoggingDto, UserLogging>();
           Mapper.CreateMap<WorkOrder, WorkOrderDto>();
           Mapper.CreateMap<WorkOrderDto, WorkOrder>();
           Mapper.CreateMap<ClientServer, ClientServerDto>();
           Mapper.CreateMap<ClientServerDto, ClientServer>();
           Mapper.CreateMap<AttributesPictureDto, Detection>();
           Mapper.CreateMap<DetectionImageDto, DetectionImage>();
           //Mapper.CreateMap<SharingDto, ShareWidget>();
           //Mapper.CreateMap<ShareWidget, SharingDto>();
       }
    }
}
