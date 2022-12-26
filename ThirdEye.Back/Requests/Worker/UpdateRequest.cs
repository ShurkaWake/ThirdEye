using System.ComponentModel.DataAnnotations;
using ThirdEye.Back.DataAccess.Entities;

using static ThirdEye.Back.Constants.Wording.WorkerWording;

namespace ThirdEye.Back.Requests.Worker
{
    public class UpdateRequest
    {
        [Required(ErrorMessage = IdRequiredMessage)]
        public int Id { get; set; }

        [Required(ErrorMessage = RoleIsRequiredMessage)]
        public Role Role { get; set; }
    }
}
