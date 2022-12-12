using Microsoft.AspNetCore.Identity;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class BusinessWorker
    {
        public int Id { get; set; }
        public User WorkerAccount { get; set; }
        public Business Job { get; set; }
        public Role WorkerRole { get; set; }
    }
}
