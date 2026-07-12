namespace Power_Fitness.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
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

        #region Create Trainer
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _trainerService.CreateTrainerAsync(model, cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
        #region Edit Trainer
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _trainerService.GetDetailedTrainersAsyncForEdit(id, cancellationToken: cancellationToken);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _trainerService.EditTrainerAsync(model, cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
        #region Delete Trainer
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _trainerService.GetDetailedTrainersAsync(id, cancellationToken: cancellationToken);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken = default)
        {
            var result = await _trainerService.DeleteTrainerAsync(id, cancellationToken: cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}