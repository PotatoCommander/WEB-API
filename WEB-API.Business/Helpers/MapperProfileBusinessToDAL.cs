using AutoMapper;
using WEB_API.Business.BusinessModels;
using WEB_API.DAL.Models;
using WEB_API.DAL.Models.Filters;

namespace WEB_API.Business.Helpers
{
    public class MapperProfileBusinessToDAL: Profile
    {
        public MapperProfileBusinessToDAL()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ProductFilter, ProductFilterModel>().ReverseMap();
            CreateMap<RatingModel, Rating>().ReverseMap();
            CreateMap<OrderDetailModel, OrderDetail>().ReverseMap();
            CreateMap<OrderModel, Order>().ReverseMap();
        }
    }
}