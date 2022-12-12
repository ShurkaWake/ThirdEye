using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.Requests.User
{
    public class LoginRequest
    {
        [EmailAddress, Required]
        public string? Email { get; set; }

        [DataType(DataType.Password), Required]
        public string? Password { get; set; }
    }
}
