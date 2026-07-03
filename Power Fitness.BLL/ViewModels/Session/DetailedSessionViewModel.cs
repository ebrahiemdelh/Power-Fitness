namespace Power_Fitness.BLL.ViewModels.Session
{
    public class DetailedSessionViewModel
    {
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string TrainerName { get; set; } = default!;

        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }
        public string StartDate { get; set; } = default!;
        public string EndDate { get; set; } = default!;
        public string Duration { get; set; } = default!;

        // TODO: Fix Status to be Ongoing, UpComing, Completed

        public string Status => AvailableSlots > 0 ? "Available" : "Full";
    }
}
