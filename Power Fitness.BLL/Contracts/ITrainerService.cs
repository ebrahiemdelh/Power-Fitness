using Power_Fitness.BLL.ViewModels.Trainer;

namespace Power_Fitness.BLL.Contracts
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken = default);
        Task<DetailedTrainerViewModel> GetDetailedTrainersAsync(int id, CancellationToken cancellationToken = default);
        Task<EditTrainerViewModel> GetDetailedTrainersAsyncForEdit(int id, CancellationToken cancellationToken = default);
        Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default);
        Task<bool> EditTrainerAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteTrainerAsync(int id, CancellationToken cancellationToken = default);
    }
}
