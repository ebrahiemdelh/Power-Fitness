using Power_Fitness.BLL.ViewModels.Trainer;

namespace Power_Fitness.BLL.Contracts
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken = default);
        Task<DetailedTrainerViewModel> GetDetailedTrainersAsync(int id, CancellationToken cancellationToken = default);
        Task<CreateTrainerViewModel> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default);
        Task<EditTrainerViewModel> EditTrainerAsync(EditTrainerViewModel model, CancellationToken cancellationToken = default);
    }
}
