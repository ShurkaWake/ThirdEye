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

        public static TimeSpan Day
        {
            get
            {
                var res = new TimeSpan(TimeSpan.TicksPerDay);
                return res;
            }
        }

        public static TimeSpan StateLiveTime
        {
            get
            {
                var res = new TimeSpan(TimeSpan.TicksPerMinute * 5);
                return res;
            }
        }
    }
}
