using System.ComponentModel.DataAnnotations;

namespace ThirdEye.Back.Requests.Business
{
    public class UpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
