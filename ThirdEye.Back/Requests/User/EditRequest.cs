using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.Requests.User
{
    public class EditRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;
    }
}
