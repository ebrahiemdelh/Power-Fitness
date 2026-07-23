namespace Power_Fitness.BLL.ViewModels.Member
{
    public class MemberHealthRecordViewModel
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = default!;
        public string Note { get; set; } = string.Empty;
    }
}
