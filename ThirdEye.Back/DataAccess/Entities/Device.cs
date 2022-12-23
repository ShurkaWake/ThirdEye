using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class Device
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public DeviceState LastState { get; set; }
        [Required]
        public Room InstalationRoom { get; set; }
    }
}
