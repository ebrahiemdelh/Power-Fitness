namespace Power_Fitness.BLL.ViewModels.Member
{
    public class EditMemberViewModel
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Photo { get; set; } = "";
        public int BuildingNo{ get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;

    }
}
