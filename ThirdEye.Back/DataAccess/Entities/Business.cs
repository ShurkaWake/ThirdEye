using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.DataAccess.Entities
{
    public class Business
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<BusinessWorker> Workers { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
