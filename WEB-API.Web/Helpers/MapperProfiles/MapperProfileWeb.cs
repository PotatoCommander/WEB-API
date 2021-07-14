using AutoMapper;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models;
using WEB_API.Web.Helpers.MapperValueResolvers;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class MapperProfileWeb: Profile
    {
        public MapperProfileWeb()
        {
            CreateMap<AddProductViewModel, ProductModel>();
            CreateMap<EditProductViewModel, ProductModel>();
            ForAllPropertyMaps(pm => pm.TypeMap.SourceType == typeof(EditProductViewModel),
                (pm, c) => c.MapFrom(new IgnoreNullResolver(), pm.SourceMember.Name));
            CreateMap<FilterProductViewModel, ProductFilterModel>().ReverseMap();
            //CreateMap<AddRatingViewModel, Rating>();
            CreateMap<ApplicationUser, UserInfoViewModel>().ReverseMap();
            CreateMap<AddRatingViewModel, RatingModel>().ReverseMap();
            CreateMap<OrderDetailViewModel, OrderDetailModel>().ReverseMap();
            CreateMap<UserLoginViewModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<OrderModel, OrderViewModel>().ReverseMap();
            CreateMap<OrderModel, OutOrderViewModel>().ReverseMap();
            CreateMap<OrderDetailModel, OutOrderDetailViewModel>();
        }
    }
}