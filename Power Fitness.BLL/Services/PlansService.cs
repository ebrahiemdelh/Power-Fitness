using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class PlansService : IPlansService
    {
        private readonly IRepository<Plan> _plansRepository;
        public PlansService(IRepository<Plan> plansRepository)
        {
            _plansRepository = plansRepository;
        }
        public async Task<IEnumerable<Plan>> GetAllPlansAsync(CancellationToken cancellationToken = default)
        {
            return await _plansRepository.GetAllAsync(cancellationToken: cancellationToken);
        }
        public async Task<Plan> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _plansRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        }
    }
}
