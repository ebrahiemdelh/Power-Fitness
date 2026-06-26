namespace Power_Fitness.DAL.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;

        public ICollection<Session> Sessions { get; set; } = default!;
    }
}
