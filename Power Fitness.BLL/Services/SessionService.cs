using Power_Fitness.BLL.ViewModels.Session;
using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default)
        {
            var sessions = await _unitOfWork.Sessions.GetSessionsWithCategoryAndTrainerAsync(cancellationToken: cancellationToken);
            var result = sessions.Select(s => new SessionViewModel
            {
                Id = s.Id,
                TrainerName = s.Trainer.Name,
                CategoryName = s.Category.Name,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacity,
                //AvailableSlots = s.AvailableSlots
            }).ToList();
            // TODO: Fix The N + 1 Query Problem
            foreach (var item in result)
            {
                item.AvailableSlots = item.Capacity - await _unitOfWork.Sessions.CountOfBookedSlots(item.Id, cancellationToken);
            }
            return result;
        }
        public Task<CreateSessionViewModel> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CreateSessionViewModel> EditSessionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CreateSessionViewModel> DeleteSessionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
