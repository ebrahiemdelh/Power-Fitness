using Power_Fitness.BLL.ViewModels.HealthRecord;
using Power_Fitness.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Power_Fitness.BLL.ViewModels.Member
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone number is required.")]
        [Length(11,11,ErrorMessage ="Phone Number Length Is 11 ")]
        [DataType(DataType.PhoneNumber)]
        //[Phone]
        public string Phone { get; set; } = default!;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateOnly DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = "Building number is required.")]
        public int BuildingNo { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = default!;


        [Required(ErrorMessage = "Health record is required.")]
        public HealthRecordViewModel HealthRecord { get; set; } = default!;

    }
}
