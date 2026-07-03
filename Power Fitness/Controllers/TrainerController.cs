namespace Power_Fitness.Controllers
{
    public class TrainerController:Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var trainers = await _trainerService.GetTrainersAsync(cancellationToken: cancellationToken);
            return View(trainers);
        }
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _trainerService.GetDetailedTrainersAsync(id, cancellationToken: cancellationToken);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }
}
}