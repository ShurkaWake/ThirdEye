using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.Constants
{
    public static class ControllerConstants
    {
        public static readonly IEnumerable<Role> roles = new[]
        {
            Role.Manager,
            Role.Worker,
        };
    }
}
