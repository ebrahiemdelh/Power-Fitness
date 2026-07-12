using Power_Fitness.BLL.ViewModels.Plan;

namespace Power_Fitness.BLL.Contracts
{
    public interface IPlansService
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync(CancellationToken cancellationToken = default);
        Task<Plan> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> UpdatePLan(int id,EditPlanViewModel model, CancellationToken cancellationToken = default);

        Task<bool> ActivateAsync(int id, CancellationToken cancellationToken = default);
    }
}