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
      CreateMap<AppUser, UserDTO>();
      // .ForMember(u => u.AvatarUrl, src => src.MapFrom(u => u.Avatar.Url));
      CreateMap<AppUser, FullUserDTO>();
      CreateMap<FullUserDTO, AppUser>();
      CreateMap<Campaign, UserCampaignDTO>();
      // CreateMap<Campaign, CampaignDetailsDTO>()
      // .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.t))

      // CreateMap<AppUser, AppUser>();
      // CreateMap<Avatar, AvatarDTO>();
      // CreateMap<Campaign, CampaignDTO>();
    }
  }
}