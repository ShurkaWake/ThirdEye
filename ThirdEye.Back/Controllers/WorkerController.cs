using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.Controllers.Abstratctions;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Extensions;
using ThirdEye.Back.Requests.Worker;

using static ThirdEye.Back.Constants.Wording.WorkerWording;
using static ThirdEye.Back.Constants.ControllerConstants;

namespace ThirdEye.Back.Controllers
{
    public class WorkerController : AuthorizedControllerBase
    {
        private readonly IStringLocalizer<WorkerController> _localizer;
        private readonly ApplicationContext _context;

        public WorkerController(IStringLocalizer<WorkerController> localizer, 
                                ApplicationContext context,
                                UserManager<User> userManager)
            :base(userManager)
        {
            _localizer = localizer;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkerAsync([FromForm] CreateRequest model)
        {
            var business = await _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == model.BusinessId);
            if(business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            var user = await _context.Users
                .Include(x => x.Works)
                .FirstOrDefaultAsync(x => x.NormalizedEmail == model.WorkerEmail.ToUpper());

            if(user is default(User))
            {
                return UserNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(x => x.WorkerAccount.Id == user.Id && x.Job.Id == business.Id);
            if (worker is not default(BusinessWorker))
            {
                return BusinessAlreadyHaveWorkerMessage.ToBadRequestUsing(_localizer);
            }

            if (!roles.Contains(model.WorkerRole))
            {
                return RoleBadRequestMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                worker = new BusinessWorker()
                {
                    Job = business,
                    WorkerAccount = user,
                    WorkerRole = model.WorkerRole,
                };

                _context.Workers.Add(worker);
                var result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return UnableToCreateMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnableToCreateMessage.ToBadRequestUsing(_localizer);
            }

            return CreatedAtAction("AddWorker", worker);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeRoleAsync([FromForm] UpdateRequest model)
        {
            var worker = await _context.Workers
                .Include(x => x.Job)
                .ThenInclude(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (worker is default(BusinessWorker))
            {
                return WorkerNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            if (!IsManagerOf(worker.Job))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            if (!roles.Contains(model.Role))
            {
                return RoleBadRequestMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                worker.WorkerRole = model.Role;
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return UnableToUpdateMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWorkerAsync([FromForm] DeleteRequest model)
        {
            var worker = await _context.Workers
                .Include(x => x.Job)
                .ThenInclude(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if(worker is default(BusinessWorker))
            {
                return WorkerNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            if (!IsManagerOf(worker.Job))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                _context.Workers.Remove(worker);
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
    }
}
