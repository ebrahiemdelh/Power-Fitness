using Power_Fitness.DAL.Enums;

namespace Power_Fitness.DAL.Models
{
    public class GymUser : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = default!;
    }


    [Owned]
    public class Address
    {
        public int BuildingNo { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}
