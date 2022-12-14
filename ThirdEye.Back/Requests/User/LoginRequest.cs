using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.Requests.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = @"""Email"" is required field")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = @"""Password"" is required field")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
