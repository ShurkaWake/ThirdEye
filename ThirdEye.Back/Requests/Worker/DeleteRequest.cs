using System.ComponentModel.DataAnnotations;

using static ThirdEye.Back.Constants.Wording.WorkerWording;

namespace ThirdEye.Back.Requests.Worker
{
    public class DeleteRequest
    {
        [Required(ErrorMessage = IdRequiredMessage)]
        public int Id { get; set; }
    }
}
