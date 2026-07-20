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

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            if (id == 0)
            {
                TempData["ErrorMessage"] = "Session Id is Invalid";
                return View(nameof(Index));
            }
            var session = await _sessionService.GetSessionByIdAsync(id, cancellationToken);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return View(nameof(Index));
            }
            return View(session);
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
            if (!ModelState.IsValid)
            {
                await GetTrainersAndCategories(cancellationToken);
                return View(model);
            }
            var res = await _sessionService.CreateSessionAsync(model, cancellationToken: cancellationToken);
            if (!res) TempData["ErrorMessage"] = "Error Creating Session.";
            else TempData["SuccessMessage"] = "Session Created Successfully.";
            return RedirectToAction(nameof(Index));
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
            if (!ModelState.IsValid)
            {
                await GetTrainersAndCategories(cancellationToken);
                return View(model);
            }
            var res = await _sessionService.EditSessionAsync(id, model, cancellationToken: cancellationToken);
            if (!res) TempData["ErrorMessage"] = "Error Updating Session.";
            else TempData["SuccessMessage"] = "Session Updated Successfully.";

            return RedirectToAction(nameof(Index));
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
            var res = await _sessionService.DeleteSessionAsync(id, cancellationToken: cancellationToken);
            if (!res) TempData["ErrorMessage"] = "Error Deleting Session.";
            else TempData["SuccessMessage"] = "Session Deleted Successfully.";
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