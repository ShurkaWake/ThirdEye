using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ThirdEye.Back.Extensions
{
    public static class StringExtensions
    {
        public static string Using(this string @this, IStringLocalizer localizer) =>
            localizer[@this].Value;

        public static BadRequestObjectResult ToBadRequestUsing(this string @this, IStringLocalizer localizer) =>
            new BadRequestObjectResult(new[] { @this.Using(localizer) });

        public static UnauthorizedObjectResult ToUnauthorizedUsing(this string @this, IStringLocalizer localizer) =>
            new UnauthorizedObjectResult(new[] { @this.Using(localizer) });

        public static ForbidResult ToForbiddedUsing(this string @this, IStringLocalizer localizer) =>
            new ForbidResult(new[] { @this.Using(localizer) });

        public static NotFoundObjectResult ToNotFoundUsing(this string @this, IStringLocalizer localizer) =>
            new NotFoundObjectResult(new[] { @this.Using(localizer) });
    }
}
