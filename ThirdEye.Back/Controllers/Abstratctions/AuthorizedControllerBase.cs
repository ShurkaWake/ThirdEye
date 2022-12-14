using ThirdEye.Back.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ThirdEye.Back.Controllers.Abstratctions
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class AuthorizedControllerBase : Controller
    {
        protected AuthorizedControllerBase()
        {

        }

        protected UserManager<User> UserManager { get; set; }
        protected async Task<User> GetUser() => await UserManager.GetUserAsync(User);
        protected string GetUserId() => UserManager.GetUserId(User);
    }
}
