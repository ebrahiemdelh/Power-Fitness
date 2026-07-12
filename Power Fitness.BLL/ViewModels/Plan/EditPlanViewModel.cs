namespace Power_Fitness.BLL.ViewModels.Plan
{
    public class EditPlanViewModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
    }
}
