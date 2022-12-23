using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.User;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.Responses.User;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;
using AutoMapper;
using ThirdEye.Back.Extensions;
using ThirdEye.Back.Services.Abstractions;
using System.Web;

using static ThirdEye.Back.Constants.Wording.UserWording;

namespace ThirdEye.Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly IStringLocalizer<UserController> _localizer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public UserController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ApplicationContext context,
                              IStringLocalizer<UserController> localizer,
                              IMapper mapper,
                              IConfiguration configuration,
                              IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [ProducesResponseType(403, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> AuthenticateAsync([FromForm] LoginRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync
                (model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            

            if (!result.Succeeded)
            {
                return InvalidLoginDataMessage.ToBadRequestUsing(_localizer);
            }

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterRequest model)
        {
            if (!model.Same())
            {
                return PasswordMissmatchMessage.ToBadRequestUsing(_localizer);
            }

            var user = _mapper.Map<User>(model);
            user.UserName = user.Email;

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.ActionLink("ConfirmEmail", "User", new { id = user.Id, token = token });

                    var message = string.Format(EmailConfirmationMessage.Using(_localizer),
                                                HtmlEncoder.Default.Encode(callbackUrl));

                    await _emailSender.SendEmailAsync(user.Email,
                                                      EmailConfirmationSubject.Using(_localizer),
                                                      message);

                    return CreatedAtAction("Register", user);
                }
                    
                return BadRequest(result.Errors.Select(e => e.Description));
            }
            catch (Exception e)
            {
                await _userManager.DeleteAsync(user);
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }
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
                return NullUserMessage.ToBadRequestUsing(_localizer);
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
                return EditErrorMessage.ToBadRequestUsing(_localizer);
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
                return NullUserMessage.ToBadRequestUsing(_localizer);
            }

            var response = new UserViewModel(user.Id,
                                             user.Email,
                                             user.EmailConfirmed,
                                             user.FirstName,
                                             user.LastName,
                                             user.Patronymic);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(302)]
        public async Task<IActionResult> ConfirmEmailAsync(string id, string token)
        {
            token = HttpUtility.HtmlDecode(token);

            string callbackPage = _configuration["MessagePage"];
            if (id == null || token == null)
            {
                var uri = new Uri(string.Format(callbackPage,
                                                InvalidUserOrTokenMessage.Using(_localizer)));
                return Redirect(uri.AbsoluteUri);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                var uri = new Uri(string.Format(callbackPage,
                                                NullUserMessage.Using(_localizer)));
                return Redirect(uri.AbsoluteUri);
            }
            else if (user.EmailConfirmed)
            {
                var uri = new Uri(string.Format(callbackPage,
                                                EmailAlreadyConfirmedMessage.Using(_localizer)));
                return Redirect(uri.AbsoluteUri);
            }

            var result = await _userManager.ConfirmEmailAsync(user!, token);
            if (!result.Succeeded)
            {
                var uri = new Uri(string.Format(callbackPage,
                                                UnexpectedErrorMessage.Using(_localizer)));
                return Redirect(uri.AbsoluteUri);
            }

            var redirect = new Uri(string.Format(callbackPage,
                                            EmailSuccessfullyConfirmedMessage.Using(_localizer)));
            return Redirect(redirect.AbsoluteUri);
        }

#if DEBUG
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = _context.Users.ToArray();
            return Ok(result);
        }
    }
#endif
}
