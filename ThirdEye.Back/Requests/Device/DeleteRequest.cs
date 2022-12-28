using System.ComponentModel.DataAnnotations;
using static ThirdEye.Back.Constants.Wording.DeviceWording;

namespace ThirdEye.Back.Requests.Device
{
    public class DeleteRequest
    {
        [Required(ErrorMessage = SerialNumberRequiredMessage)]
        public string SerialNumber { get; set; }
    }
}
