using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class AddProductViewModelProfile: Profile
    {
        public AddProductViewModelProfile()
        {
            CreateMap<AddProductViewModel, Product>();
        }
    }
}