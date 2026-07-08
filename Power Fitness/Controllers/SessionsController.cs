using Microsoft.AspNetCore.Mvc.Rendering;

namespace Power_Fitness.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken: cancellationToken);
            return View(sessions);
        }
        #region Create Session
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            await GetTrainersAndCategories(cancellationToken);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _sessionService.CreateSessionAsync(model, cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            await GetTrainersAndCategories(cancellationToken);
            return View(model);
        }
        #endregion
        #region Edit Session
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionService.GetSessionForUpdateAsync(id, cancellationToken: cancellationToken);
            if (session == null)
            {
                return NotFound();
            }

            await GetTrainersAndCategories(cancellationToken);
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSessionViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _sessionService.EditSessionAsync(id, model, cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            await GetTrainersAndCategories(cancellationToken);

            return View(model);
        }
        #endregion

        #region Delete Session
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionService.GetSessionByIdAsync(id, cancellationToken: cancellationToken);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken = default)
        {
            await _sessionService.DeleteSessionAsync(id, cancellationToken: cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private async Task GetTrainersAndCategories(CancellationToken cancellationToken)
        {
            var categories = await _sessionService.GetCategories(cancellationToken: cancellationToken);
            var categoriesList = new SelectList(categories, "Key", "Value");

            var trainers = await _sessionService.GetTrainers(cancellationToken: cancellationToken);
            var trainersList = new SelectList(trainers, "Key", "Value");

            ViewBag.Categories = categoriesList;
            ViewBag.Trainers = trainersList;
        }


    }
}