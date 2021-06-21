using AutoMapper;

namespace WEB_API.Web.Helpers.MapperValueResolvers
{
    class IgnoreNullResolver : IMemberValueResolver<object, object, object, object>
    {
        public object Resolve(object source, object destination, object sourceMember, object destinationMember, ResolutionContext context)
        {
            return sourceMember ?? destinationMember;
        }
    }
}