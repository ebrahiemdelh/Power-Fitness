using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.HealthRecord
{
    public class HealthRecordViewModel
    {
        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300.")]
        [Required(ErrorMessage =("Height Is Required"))]
        public decimal Height { get; set; }

        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500.")]
        [Required(ErrorMessage = ("Weight Is Required"))]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood type is required.")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "Blood type must be between 1 and 3 characters.")]
        public string BloodType { get; set; } = default!;

        [StringLength(250, ErrorMessage = "Note cannot exceed 250 characters.")]
        public string? Note { get; set; } = default!;
    }
}
