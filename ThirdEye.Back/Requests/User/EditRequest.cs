using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.UserWording;

namespace ThirdEye.Back.Requests.User
{
    public class EditRequest
    {
        [Required(ErrorMessage = FirstNameRequiredMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = LastNameRequiredMessage)]
        public string LastName { get; set; }

        public string Patronymic { get; set; } = string.Empty;
    }
}
