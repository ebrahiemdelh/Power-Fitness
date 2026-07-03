using Power_Fitness.BLL.ViewModels.Session;

namespace Power_Fitness.BLL.Contracts
{
    public interface ISessionService
    {
        Task<List<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default);

        Task<CreateSessionViewModel> CreateSessionAsync(CancellationToken cancellationToken = default);
        Task<CreateSessionViewModel> EditSessionAsync(CancellationToken cancellationToken = default);
        Task<CreateSessionViewModel> DeleteSessionAsync(CancellationToken cancellationToken = default);
    }
}
