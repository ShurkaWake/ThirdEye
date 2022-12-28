using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThirdEye.Back.DataAccess.Entities
{
    [Index(nameof(Device.SerialNumber), IsUnique = true)]
    public class Device
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        [JsonIgnore]
        public Room InstalationRoom { get; set; }
    }
}
