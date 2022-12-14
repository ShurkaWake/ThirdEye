using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.UserWording;

namespace ThirdEye.Back.Requests.User
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = EmailRequiredMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = PasswordRequiredMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = RepeatPasswordRequiredMessage)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = FirstNameRequiredMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = LastNameRequiredMessage)]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public bool Same() => Password == ConfirmPassword;
    }
}
