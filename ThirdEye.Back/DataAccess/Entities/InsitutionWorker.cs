using Microsoft.AspNetCore.Identity;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class InsitutionWorker
    {
        public int Id { get; set; }
        public User WorkerAccount { get; set; }
        public Institution Job { get; set; }
        public IdentityRole Role { get; set; }
    }
}
