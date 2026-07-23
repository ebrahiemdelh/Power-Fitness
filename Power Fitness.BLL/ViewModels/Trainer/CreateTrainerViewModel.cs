using Power_Fitness.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Trainer
{
    public class CreateTrainerViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^01\d{9}$", ErrorMessage = "Invalid Phone Format")]
        [StringLength(11, ErrorMessage = ("Phone Length Is only 11"))]
        public string Phone { get; set; } = default!;

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public Specialties Specialties { get; set; }

        [Required(ErrorMessage = "Gender is required]")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;
    }
}
