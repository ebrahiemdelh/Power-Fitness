namespace Power_Fitness.DAL.Dtos.Members
{
    public class PartialMemberShipData
    {
        public string PlanName { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateOnly MembershipEndDate { get; set; }
    }
}
