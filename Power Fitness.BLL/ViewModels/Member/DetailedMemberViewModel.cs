using System.ComponentModel;

namespace Power_Fitness.BLL.ViewModels.Member
{
    public class DetailedMemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        [DisplayName("Date Of Birth")]
        public string DOB { get; set; } = default!;

        public string Gender { get; set; } = default!;

        public string Photo { get; set; } = "";

        public string Address { get; set; } = default!;

        public string PlanName { get; set; } = default!;
        public string MembershipStartDate { get; set; } = default!;
        public string MembershipEndDate { get; set; } = default!;

    }
}
