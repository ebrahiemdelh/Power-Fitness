using System.ComponentModel.DataAnnotations.Schema;

namespace Power_Fitness.DAL.Models
{
    public class Membership : BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = default!;

        // StartDate => BaseEntity.CreatedAt
        public DateOnly EndDate => DateOnly.FromDateTime(CreatedAt.AddDays(Plan.DurationDays));

        [NotMapped]
        public bool IsActive => EndDate >= DateOnly.FromDateTime(DateTime.Now);
        [NotMapped]
        public string Status => IsActive ? "Active" : "Inactive";
    }
}
