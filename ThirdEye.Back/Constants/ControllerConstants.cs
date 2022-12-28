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

        public static TimeSpan ThirtyDays
        {
            get
            {
                var res = new TimeSpan(TimeSpan.TicksPerDay * 30);
                return res;
            }
        }
    }
}
