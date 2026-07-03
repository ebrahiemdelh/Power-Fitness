using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Session
{
    public class EditSessionViewModel
    {
        public int SessionId { get; set; }
        [Required(ErrorMessage = "Trainer is required.")]
        public int TrainerId { get; set; }
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
