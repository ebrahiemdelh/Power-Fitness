using Power_Fitness.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Trainer
{
    public class EditTrainerViewModel
    {
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;


        [Required(ErrorMessage = "Phone is required")]
        [Length(11,11,ErrorMessage ="phone is 11")]
        public string Phone { get; set; } = default!;

        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "Specialization is required")]
        public Specialties Specialties { get; set; } = default!;
    }
}
