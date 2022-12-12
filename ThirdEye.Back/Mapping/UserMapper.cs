using AutoMapper;
using ThirdEye.Back.Requests.User;
using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<RegisterRequest, User>();
        }
    }
}
