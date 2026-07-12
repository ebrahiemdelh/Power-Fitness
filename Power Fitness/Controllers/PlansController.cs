using Power_Fitness.BLL.ViewModels.Plan;

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
            var model = new EditPlanViewModel()
            {
                Name=plan.Name,
                Description=plan.Description,
                Price=plan.Price,
                DurationDays=plan.DurationDays,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPlanViewModel model, CancellationToken cancellationToken)
        {
            var result = await _plansService.UpdatePLan(id, model, cancellationToken);
            if (result)
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            else
                TempData["ErrorMessage"] = "Error Updating Plan";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Activate(int id, CancellationToken cancellationToken)
        {
            var result = await _plansService.ActivateAsync(id, cancellationToken);
            if (result) TempData["SuccessMessage"] = "Activation Status Changed Successfully";
            else TempData["ErrorMessage"] = "Error Changing Activation Status";
            return RedirectToAction("index");
        }
    }
}
