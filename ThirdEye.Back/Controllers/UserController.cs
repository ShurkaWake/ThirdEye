﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.User;
using ThirdEye.Back.Mapping;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.Responses.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ThirdEye.Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ApplicationContext context,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> AuthenticateAsync([FromForm] LoginRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync
                (model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            return result.Succeeded ? Ok() : BadRequest("Invalid login and/or password");
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        [Authorize]
        [HttpPatch]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [ProducesResponseType(401, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> EditProfileAsync([FromForm] EditRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("No such user");
            }

            if(!string.IsNullOrEmpty(request.FirstName))
            {
                user.FirstName = request.FirstName;
            }
            if(!string.IsNullOrEmpty(request.LastName))
            {
                user.LastName = request.LastName;
            }
            if(!string.IsNullOrEmpty(request.Patronymic))
            {
                user.Patronymic = request.Patronymic;
            }

            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync() > 0;
            if (result)
            {
                return CreatedAtAction("EditProfile", user);
            }
            else
            {
                return BadRequest("Unable to edit this user");
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [ProducesResponseType(401, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<UserViewModel>> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user is null)
            {
                return BadRequest("No such user");
            }

            var response = new UserViewModel(user.Id,
                                             user.Email,
                                             user.FirstName,
                                             user.LastName,
                                             user.Patronymic);
            return Ok(response);
        }
    }
}
