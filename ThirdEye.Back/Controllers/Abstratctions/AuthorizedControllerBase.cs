using ThirdEye.Back.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ThirdEye.Back.Controllers.Abstratctions
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public abstract class AuthorizedControllerBase : Controller
    {
        protected AuthorizedControllerBase(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        protected UserManager<User> UserManager { get; set; }
        protected async Task<User> GetUser() => await UserManager.GetUserAsync(User);
        protected string GetUserId() => UserManager.GetUserId(User);

        protected bool IsWorkerOf(Business business)
        {
            var userId = UserManager.GetUserId(User);
            var res = business.Workers.FirstOrDefault
                (x => x.WorkerAccount.Id == userId);

            return res is not default(BusinessWorker);
        }

        protected bool IsManagerOf(Business business)
        {
            var userId = UserManager.GetUserId(User);
            var res = business.Workers.FirstOrDefault
                (x => x.WorkerAccount.Id == userId && x.WorkerRole == Role.Manager);

            return res is not default(BusinessWorker);
        }
    }
}
