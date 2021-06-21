using System;
using AutoMapper;
using WEB_API.DAL.Models;
using WEB_API.Web.Helpers.MapperValueResolvers;
using WEB_API.Web.ViewModels;

namespace WEB_API.Web.Helpers.MapperProfiles
{
    public class EditProductViewModelProfile: Profile
    {
        public EditProductViewModelProfile()
        {
            CreateMap<EditProductViewModel, Product>();
            ForAllPropertyMaps(pm => pm.TypeMap.SourceType == typeof(EditProductViewModel),
            (pm, c) => c.MapFrom(new IgnoreNullResolver(), pm.SourceMember.Name));
        }
    }
}