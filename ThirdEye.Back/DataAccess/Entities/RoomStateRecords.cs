using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class RoomStateRecords
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public RoomState State { get; set; }

        [Required]
        public int StateTimeSeconds { get; set; }

        [Required]
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
