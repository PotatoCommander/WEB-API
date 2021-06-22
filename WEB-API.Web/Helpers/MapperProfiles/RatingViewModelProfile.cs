using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class RatingViewModelProfile:Profile
    {
        public RatingViewModelProfile()
        {
            CreateMap<AddRatingViewModel, Rating>();
        }
    }
}