using Power_Fitness.BLL.ViewModels.Trainer;
using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken = default)
        {
            var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(cancellationToken: cancellationToken);
            var result = _mapper.Map<IEnumerable<TrainerViewModel>>(trainers);
            return result;
        }
        public async Task<DetailedTrainerViewModel> GetDetailedTrainersAsync(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken);
            if (trainer == null)
            {
                return null;
            }

            return _mapper.Map<DetailedTrainerViewModel>(trainer);
        }
        public async Task<EditTrainerViewModel> GetDetailedTrainersAsyncForEdit(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken);
            if (trainer == null)
            {
                return null;
            }

            return _mapper.Map<EditTrainerViewModel>(trainer);
        }
        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            var trainer = _mapper.Map<Trainer>(model);
            return await _unitOfWork.GetRepository<Trainer>().AddAsync(trainer, cancellationToken) > 0;
        }

        public async Task<bool> EditTrainerAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken);
            if (trainer is null) return false;
            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.BuildingNo = model.BuildingNumber;
            trainer.Address.Street = model.Street;
            trainer.Address.City = model.City;
            trainer.Specialty = model.Specialties;
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> DeleteTrainerAsync(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken);
            if (trainer == null)
                return false;
            var result = await _unitOfWork.GetRepository<Trainer>().DeleteAsync(trainer, cancellationToken);
            return result > 0;
        }
    }
}
