namespace Power_Fitness.BLL.Contracts
{
    public interface IPlansService
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync(CancellationToken cancellationToken = default);
        Task<Plan> GetPlanByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}