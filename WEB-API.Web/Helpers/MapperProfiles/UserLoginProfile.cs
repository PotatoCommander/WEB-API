using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.DAL.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class UserLoginProfile: Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserLoginViewModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}