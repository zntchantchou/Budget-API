using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<AppUser, UserDTO>()
      .ForMember(u => u.AvatarUrl, src => src.MapFrom(u => u.Avatar.Url));
      CreateMap<Avatar, AvatarDTO>();
      CreateMap<AppUser, FullUserDTO>();
      CreateMap<Campaign, CampaignDTO>();
    }
  }
}