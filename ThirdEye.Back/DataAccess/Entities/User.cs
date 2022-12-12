using Microsoft.AspNetCore.Identity;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<BusinessWorker> Works { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
