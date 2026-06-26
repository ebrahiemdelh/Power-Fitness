namespace Power_Fitness.DAL.Models
{
    public class Member : GymUser
    {
        public string Photo { get; set; } = default!;

        public HealthRecord HealthRecord { get; set; } = default!;

        public ICollection<Membership> Memberships { get; set; } = default!;

        public ICollection<Booking> Bookings { get; set; } = default!;
    }
}
