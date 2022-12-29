using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.Requests.DeviceState
{
    public class PostStateRequest
    {
        public string SerialNumber { get; set; }
        public RoomState State { get; set; }
    }
}
