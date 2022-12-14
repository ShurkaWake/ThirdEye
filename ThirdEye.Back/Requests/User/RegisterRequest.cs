using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.Requests.User
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = @"Email is required field")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = @"""Password"" is required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = @"""Repeat password"" is required field")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = @"""First name"" is required field")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = @"""Last name"" is required field")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public bool Same() => Password == ConfirmPassword;
    }
}
