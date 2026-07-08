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
        public async Task<SessionViewModel> GetSessionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id, cancellationToken: cancellationToken);
            if (session == null) return null;
            var result = new SessionViewModel
            {
                Id = session.Id,
                TrainerName = session.Trainer.Name,
                CategoryName = session.Category.Name,
                Description = session.Description,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Capacity = session.Capacity,
                AvailableSlots = session.Capacity - await _unitOfWork.Sessions.CountOfBookedSlots(session.Id, cancellationToken)
            };
            return result;
        }
        public async Task<bool> CreateSessionAsync(CreateSessionViewModel createSession, CancellationToken cancellationToken = default)
        {
            if (createSession.StartDate > createSession.EndDate) return false;

            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(createSession.TrainerId, cancellationToken);
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(createSession.CategoryId, cancellationToken);

            if (string.Compare(category.Name, trainer.Specialty.ToString(), true) == 0) return false;
            //TODO: Check if the trainer is available for the session time

            var session = new Session
            {
                Capacity = createSession.Capacity,
                CategoryId = createSession.CategoryId,
                Description = createSession.Description,
                StartDate = createSession.StartDate,
                EndDate = createSession.EndDate,
                TrainerId = createSession.TrainerId,
            };
            var result = await _unitOfWork.GetRepository<Session>().AddAsync(session, cancellationToken);
            if (result > 1) return true;
            return false;
        }

        public Task<bool> EditSessionAsync(EditSessionViewModel editSession, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSessionAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, string>> GetCategories(CancellationToken cancellationToken = default)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(cancellationToken: cancellationToken);
            return categories.ToDictionary(c => c.Id, c => c.Name);
        }

        public async Task<Dictionary<int, string>> GetTrainers(CancellationToken cancellationToken = default)
        {
            var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(cancellationToken: cancellationToken);
            return trainers.ToDictionary(t => t.Id, t => t.Name);
        }
    }
}
