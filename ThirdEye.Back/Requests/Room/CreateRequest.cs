using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.RoomWording;

namespace ThirdEye.Back.Requests.Room
{
    public class CreateRequest
    {
        [Required(ErrorMessage = NameRequiredMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = BusinessIdRequiredMessage)]
        public int BusinessId { get; set; }
    }
}
