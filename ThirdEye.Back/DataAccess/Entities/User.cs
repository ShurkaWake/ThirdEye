using Microsoft.AspNetCore.Identity;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<InsitutionWorker> Works { get; set; }
    }
}
