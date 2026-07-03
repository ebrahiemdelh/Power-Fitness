namespace Power_Fitness.BLL.ViewModels.Trainer
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
