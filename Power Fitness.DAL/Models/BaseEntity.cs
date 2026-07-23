namespace Power_Fitness.DAL.Models
{
    public class BaseEntity
    //internal class BaseEntity<Key> //if Id is different based on entity
    {
        public int Id { get; set; }
        //public KeyId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
