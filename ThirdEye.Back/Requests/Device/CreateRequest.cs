using System.ComponentModel.DataAnnotations;
using static ThirdEye.Back.Constants.Wording.DeviceWording;

namespace ThirdEye.Back.Requests.Device
{
    public class CreateRequest
    {
        [Required(ErrorMessage = SerialNumberRequiredMessage)]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = RoomIdRequiredMessage)]
        public int RoomId { get; set; }
    }
}
