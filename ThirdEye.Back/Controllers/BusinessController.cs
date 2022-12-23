using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.Controllers.Abstratctions;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.Requests.Business;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using static ThirdEye.Back.Constants.Wording.BusinessWording;

namespace ThirdEye.Back.Controllers
{
    public class BusinessController : AuthorizedControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<BusinessController> _localizer;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public BusinessController(IConfiguration configuration,
                                  IStringLocalizer<BusinessController> localizer,
                                  IMapper mapper,
                                  ApplicationContext context,
                                  UserManager<User> userManager)
            : base(userManager)
        {
            _configuration = configuration;
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusinessAsync([FromForm] CreateRequest model)
        {
            var user = await UserManager.GetUserAsync(User);
            var business = _mapper.Map<Business>(model);
            business.Workers = new List<BusinessWorker>()
            {
                new BusinessWorker()
                {
                    Job = business,
                    WorkerAccount = user,
                    WorkerRole = Role.Manager,
                }
            };

            try
            {
                _context.Businesses.Add(business);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return UnableToCreateMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch
            {
                UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return CreatedAtAction("CreateBusiness", business);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusinessAsync(int id)
        {
            var business = _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefault(x => x.Id == id);

            if (business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }


            if (!IsWorkerOf(business))
            {
                return UserIsNotWorkerMessage.ToBadRequestUsing(_localizer);
            }

            return Ok(business);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBusinessAsync()
        {
            var user = await UserManager.GetUserAsync(User);

            var result = _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .Where(x => (x.Workers.Where(x => x.WorkerAccount.Id == user.Id).Count() > 0))
                .ToArray();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBusinessAsync(int id)
        {
            var business = _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefault(x => x.Id == id);

            if(business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }
            if (!IsManagerOf(business))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                _context.Businesses.Remove(business);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return UnableToDeleteMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBusinessAsync([FromForm] UpdateRequest model)
        {
            var business = _context.Businesses.FirstOrDefault(x => x.Id == model.Id);
            if(business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            try
            {
                business.Name = model.Name;
                var isOk = await _context.SaveChangesAsync() > 0;
                if (!isOk)
                {
                    return UnableToUpdateMessage.ToNotFoundUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return Ok(business);
        }

        private bool IsWorkerOf(Business business)
        {
            var userId = UserManager.GetUserId(User);
            var res = business.Workers.FirstOrDefault
                (x => x.WorkerAccount.Id == userId);

            return res is not default(BusinessWorker);
        }

        private bool IsManagerOf(Business business)
        {
            var userId = UserManager.GetUserId(User);
            var res = business.Workers.FirstOrDefault
                (x => x.WorkerAccount.Id == userId && x.WorkerRole == Role.Manager);

            return res is not default(BusinessWorker);
        }
    }
}
