using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Business BusinessLocated { get; set; }
        public IEnumerable<RoomStateChange> StateChanges { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
