using System.ComponentModel.DataAnnotations;
using ThirdEye.Back.DataAccess.Entities;

using static ThirdEye.Back.Constants.Wording.WorkerWording;

namespace ThirdEye.Back.Requests.Worker
{
    public class CreateRequest
    {
        [Required(ErrorMessage = BusinessIdRequiredMessage)]
        public int BusinessId { get; set; }

        [Required(ErrorMessage = WorkerEmailRequiredMessage)]
        public string WorkerEmail { get; set; }
        
        [Required(ErrorMessage = RoleIsRequiredMessage)]
        public Role WorkerRole { get; set; }
    }
}
