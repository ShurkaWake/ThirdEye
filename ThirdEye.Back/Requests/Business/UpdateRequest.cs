using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.BusinessWording;

namespace ThirdEye.Back.Requests.Business
{
    public class UpdateRequest
    {
        [Required(ErrorMessage = IdRequiredMessage)]
        public int Id { get; set; }

        [Required(ErrorMessage = NameRequiredMessage)]
        public string Name { get; set; }
    }
}
