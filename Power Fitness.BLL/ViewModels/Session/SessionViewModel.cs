namespace Power_Fitness.BLL.ViewModels.Session
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string TrainerName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; } = default!;
        //public string DateDisplay { get; set; } = default!;
        //public string TimeRangeDisplay { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration => EndDate - StartDate;
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }
        // TODO: Fix Status To Be Ongoing, Upcoming, or Completed based on the current date and time compared to the session's date and time.
        public string Status => DateTime.Now < StartDate ? "Upcoming" : (DateTime.Now > EndDate ? "Completed" : "Ongoing");
    }
}
