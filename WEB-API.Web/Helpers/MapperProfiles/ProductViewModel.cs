using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.DAL.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class ProductViewModel: Profile
    {
        public ProductViewModel()
        {
            CreateMap<AddProductViewModel, Product>();
        }
    }
}