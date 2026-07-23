using Power_Fitness.DAL.Enums;

namespace Power_Fitness.DAL.Models
{
    public class Trainer : GymUser
    {
        public Specialties Specialty { get; set; } = default!;
        public DateTime HireDate { get; set; }=DateTime.Now;

        public ICollection<Session> Sessions { get; set; } = default!;
    }
}
