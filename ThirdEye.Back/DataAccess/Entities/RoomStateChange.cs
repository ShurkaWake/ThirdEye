using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class RoomStateChange
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime ChangeTime { get; set; }
        [Required]
        public RoomState State { get; set; }
        [Required]
        public Room RoomChanged { get; set; }
    }
}
