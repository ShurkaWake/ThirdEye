using AutoMapper;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.Business;

namespace ThirdEye.Back.Mapping
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<CreateRequest, Business>();
        }
    }
}
