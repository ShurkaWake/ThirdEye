using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class BusinessWorker
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public User WorkerAccount { get; set; }

        [Required]
        [JsonIgnore]
        public Business Job { get; set; }
        [Required]
        public Role WorkerRole { get; set; }
    }
}
