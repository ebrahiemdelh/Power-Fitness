using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Session
{
    public class CreateSessionViewModel
    {
        [StringLength(200, ErrorMessage = "The Description must be at most 200 characters long.")]
        public string Description { get; set; } = default!;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a trainer.")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "The Start Date & Time field is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The End Date & Time field is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The Capacity field is required.")]
        [Range(1, 25, ErrorMessage = "Capacity must be between 1 and 25.")]
        public int Capacity { get; set; }
    }
}
