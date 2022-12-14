using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdEye.Back.Controllers.Abstratctions;
using ThirdEye.Back.DataAccess.Contexts;

namespace ThirdEye.Back.Controllers
{
    public class BusinessController : AuthorizedControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;
    }
}
