using Power_Fitness.BLL.ViewModels.Plan;
using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class PlansService : IPlansService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlansService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Plan>> GetAllPlansAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetRepository<Plan>().GetAllAsync(cancellationToken: cancellationToken);
        }
        public async Task<Plan> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.GetRepository<Plan>().GetByIdAsync(id, cancellationToken: cancellationToken);
        }
        public async Task<bool> UpdatePLan(int id, EditPlanViewModel model, CancellationToken cancellationToken = default)
        {
            var plan = await _unitOfWork.Plans.GetPLanWithMemberships(id, cancellationToken);
            if (plan is null) return false;
            if (plan.Memberships is not null) return false;
            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;
            plan.Description = model.Description;
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;


        }
        public async Task<bool> ActivateAsync(int id, CancellationToken cancellationToken = default)
        {
            var plan = await GetPlanByIdAsync(id, cancellationToken);
            if (plan is null) return false;

            plan.IsActive = !plan.IsActive;
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }

    }
}
