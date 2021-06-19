using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;
using WEB_API.DAL.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class FilterProductProfile: Profile
    {
        public FilterProductProfile()
        {
            CreateMap<FilterProductViewModel, ProductFilter>().ReverseMap();
        }
    }
}