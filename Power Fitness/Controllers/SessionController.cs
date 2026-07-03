namespace Power_Fitness.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken: cancellationToken);
            return View(sessions);
        }
        #region Create Session
        public IActionResult Create(CancellationToken cancellationToken = default)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                await _sessionService.CreateSessionAsync(cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
        #region Edit Session
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var session = await _sessionService.EditSessionAsync(cancellationToken: cancellationToken);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSessionViewModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                //await _sessionService.UpdateSessionAsync(cancellationToken: cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

    }
}