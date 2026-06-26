namespace Power_Fitness.DAL.Models
{
    public class HealthRecord : BaseEntity
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string BloodType { get; set; } = default!;
        public string Note { get; set; } = default!;
        
        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;
    }
}
