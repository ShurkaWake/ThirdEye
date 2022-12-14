using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.UserWording;

namespace ThirdEye.Back.Requests.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = EmailRequiredMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = PasswordRequiredMessage)]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
