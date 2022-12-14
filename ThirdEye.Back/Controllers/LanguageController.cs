using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ThirdEye.Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        public IActionResult SetLanguage(string culture)
        {
            try
            {
                Response.Cookies.Append(key: CookieRequestCultureProvider.DefaultCookieName,
                                        value: CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                        options: new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
