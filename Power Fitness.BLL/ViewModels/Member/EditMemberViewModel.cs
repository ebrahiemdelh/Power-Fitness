using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Member
{
    public class EditMemberViewModel
    {
        public string? Name { get; set; } = default!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone Is Required")]
        [RegularExpression(@"^01\d{9}$", ErrorMessage = "Invalid Phone Format")]
        public string Phone { get; set; } = default!;

        public string Photo { get; set; } = "";

        [Required(ErrorMessage = "Building No. is Required")]
        public int BuildingNo { get; set; } = default!;

        [Required(ErrorMessage = "Street is Required")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; } = default!;
    }
}
