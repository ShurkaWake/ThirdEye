using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public IEnumerable<BusinessWorker> Works { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
