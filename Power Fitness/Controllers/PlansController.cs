namespace Power_Fitness.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlansService _plansService;
        public PlansController(IPlansService plansService) //ctor injection
        {
            _plansService = plansService;
        }

        [HttpGet]
        //public async Task<IActionResult> Index(CancellationToken cancellationToken, [FromServices]PlansService plansService) method injection
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var plans = await _plansService.GetAllPlansAsync(cancellationToken: cancellationToken);

            return View(plans);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var plan = await _plansService.GetPlanByIdAsync(id, cancellationToken: cancellationToken);
            if (plan is null) return NotFound();
            return View(plan);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var plan = await _plansService.GetPlanByIdAsync(id, cancellationToken: cancellationToken);
            if (plan is null) return NotFound();
            return View(plan);
        }
        //[HttpPost]
        //public async Task<IActionResult> Activate(int id, CancellationToken cancellationToken)
        //{
        //    var plan = await _plansService.GetPlanByIdAsync(id, cancellationToken: cancellationToken);
        //    if (plan is null) return NotFound();
        //    plan.IsActive = !plan.IsActive;
        //    plan.UpdatedAt = DateTime.Now;
        //    _dbContext.SaveChanges();
        //    return RedirectToAction("index");
        //}
    }
}
