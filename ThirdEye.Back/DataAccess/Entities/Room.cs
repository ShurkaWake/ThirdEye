using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [JsonIgnore]
        public Business BusinessLocated { get; set; }

        [Required]
        public RoomState CurrentState { get; set; }

        [Required]
        public DateTime LastDeviceResponceTime { get; set; } 
        
        public IEnumerable<RoomStateRecords> StateRecords { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
