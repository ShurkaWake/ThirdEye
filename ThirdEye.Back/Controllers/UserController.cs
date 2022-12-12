using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.User;
using ThirdEye.Back.Mapping;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using AutoMapper;

namespace ThirdEye.Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSmth()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync([FromForm] LoginRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync
                (model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            return result.Succeeded ? Ok() : BadRequest("Invalid login and/or password");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterRequest model)
        {
            if (model.Same())
            {
                var user = _mapper.Map<User>(model);
                user.UserName = user.Email;
                try
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return CreatedAtAction("Register", user);
                    }
                    return BadRequest(result.Errors.Select(e => e.Description));
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest("Something went wrong. Please try again");
                }
            }
            return BadRequest("Passwords are not the same");
        }
    }
}
