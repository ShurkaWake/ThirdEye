using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.BusinessWording;

namespace ThirdEye.Back.Requests.Business
{
    public class CreateRequest
    {
        [Required(ErrorMessage = NameRequiredMessage)]
        public string Name { get; set; }
    }
}
