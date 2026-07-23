namespace Power_Fitness.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default)
        {
            var sessions = await _unitOfWork.Sessions.GetSessionsWithCategoryAndTrainerAsync(cancellationToken: cancellationToken);
            var result = _mapper.Map<List<SessionViewModel>>(sessions);

            var bookedSessionsCount = await _unitOfWork.Sessions.SessionsBookedSlots(cancellationToken);
            foreach (var item in result)
            {
                item.AvailableSlots = item.Capacity - bookedSessionsCount.GetValueOrDefault(item.Id, 0);
            }
            return result;
        }
        public async Task<SessionViewModel> GetSessionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.Sessions.GetSessionWithCategoryAndTrainerAsync(id, cancellationToken: cancellationToken);
            if (session == null) return null!;
            var result = _mapper.Map<SessionViewModel>(session);
            result.AvailableSlots = session.Capacity - await _unitOfWork.Sessions.CountOfBookedSlots(session.Id, cancellationToken);
            return result;
        }
        public async Task<bool> CreateSessionAsync(CreateSessionViewModel createSession, CancellationToken cancellationToken = default)
        {
            if (createSession.StartDate > createSession.EndDate) return false;

            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(createSession.TrainerId, cancellationToken);
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(createSession.CategoryId, cancellationToken);

            if (!string.Equals(category?.Name, trainer?.Specialty.ToString(), StringComparison.OrdinalIgnoreCase))
                return false;

            var trainerHasConflict = await _unitOfWork.AnyAsync<Session>(s =>
                        s.TrainerId == createSession.TrainerId &&
                        createSession.StartDate < s.EndDate &&
                        createSession.EndDate > s.StartDate,
                        cancellationToken);

            if (trainerHasConflict)
                return false;

            var session = _mapper.Map<Session>(createSession);
            var result = await _unitOfWork.GetRepository<Session>().AddAsync(session, cancellationToken);
            if (result > 1) return true;
            return false;
        }
        public async Task<EditSessionViewModel> GetSessionForUpdateAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id == 0) return null!;
            var session = await _unitOfWork.Sessions.GetWithCategoryByIdAsync(id, cancellationToken);
            if (session == null) return null!;
            var result = new EditSessionViewModel
            {
                TrainerId = session.TrainerId,
                Description = session.Description,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
            };
            return result;
        }

        public async Task<bool> EditSessionAsync(int id, EditSessionViewModel editSession, CancellationToken cancellationToken = default)
        {
            if (id == 0) return false;
            var session = await _unitOfWork.Sessions.GetWithCategoryByIdAsync(id, cancellationToken);
            if (session == null) return false;


            if (editSession.StartDate > editSession.EndDate) return false;
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(editSession.TrainerId, cancellationToken);

            if (!string.Equals(session.Category.Name, trainer?.Specialty.ToString(), StringComparison.OrdinalIgnoreCase))
                return false;

            var trainerHasConflict = await _unitOfWork.AnyAsync<Session>(s =>
            s.TrainerId == editSession.TrainerId &&
            editSession.StartDate < s.EndDate &&
            editSession.EndDate > s.StartDate,
            cancellationToken);

            session.TrainerId = editSession.TrainerId;
            session.Description = editSession.Description;
            session.StartDate = editSession.StartDate;
            session.EndDate = editSession.EndDate;
            var result = await _unitOfWork.GetRepository<Session>().UpdateAsync(session, cancellationToken);
            return result > 0;
        }

        public async Task<bool> DeleteSessionAsync(int id, CancellationToken cancellationToken = default)
        {
            var session = await _unitOfWork.GetRepository<Session>().GetByIdAsync(id, cancellationToken);
            if (session == null) return false;

            var result = await _unitOfWork.GetRepository<Session>().DeleteAsync(session, cancellationToken);
            return result > 0;
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
