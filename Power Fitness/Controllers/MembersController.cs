using Microsoft.AspNetCore.Mvc;
using Power_Fitness.BLL.Contracts;
using Power_Fitness.BLL.ViewModels.Member;

namespace Power_Fitness.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var members = await _membersService.GetAllMembersAsync(cancellationToken);
            return View(members);
        }

        #region Create
        [HttpGet]
        public IActionResult Create(CancellationToken cancellationToken = default)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _membersService.CreateMemberAsync(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Email or Phone already exists.");
                return View(model);
            }
            TempData["SuccessMessage"] = "Member Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            var member = await _membersService.GetMemberAsync(id, cancellationToken);

            if (member == null) return NotFound();

            return View(member);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var member = await _membersService.GetMemberAsync(id, cancellationToken);
            if (member == null) return NotFound();
            var EditMember = new EditMemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                //BuildingNo = member.Address.BuildingNo
            };
            return View(EditMember);
        }
        public async Task<IActionResult> Edit(int id, EditMemberViewModel editMember, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return View(editMember);

            var result = await _membersService.UpdateMemberAsync(id, editMember, cancellationToken);
            if (!result)
            {
                TempData["ErrorMessage"] = "Email Or Password Already Used";
                return View(editMember);
            }

            TempData["SuccessMessage"] = "Member Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region HealthRecord
        public async Task<IActionResult> HealthRecordDetails(int id,CancellationToken cancellationToken=default)
        {
            var healthRecordVM = await _membersService.GetHealthRecord(id,cancellationToken);
            if (healthRecordVM == null) return NotFound();
            return View(healthRecordVM);
        }
        #endregion

    }
}
