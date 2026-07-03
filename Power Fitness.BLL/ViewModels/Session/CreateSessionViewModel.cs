using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Session
{
    public class CreateSessionViewModel
    {
        [StringLength(200, ErrorMessage = "The Description must be at most 200 characters long.")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "The Category field is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The Trainer field is required.")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "The Start Date & Time field is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The End Date & Time field is required.")]
        public DateTime EndDate { get; set; }

        [MinLength(1, ErrorMessage = "The Capacity must be greater than 0.")]
        public int Capacity { get; set; }
    }
}
