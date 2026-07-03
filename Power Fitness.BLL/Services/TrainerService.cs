using Power_Fitness.BLL.ViewModels.Trainer;
using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TrainerViewModel>> GetTrainersAsync(CancellationToken cancellationToken = default)
        {
            var trainers = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(cancellationToken: cancellationToken);
            var result = trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Gender = t.Gender.ToString(),
                Phone = t.Phone,
                ImageUrl = ""
            });
            return result;
        }

        public async Task<DetailedTrainerViewModel> GetDetailedTrainersAsync(int id, CancellationToken cancellationToken = default)
        {
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(id, cancellationToken);
            if (trainer == null)
            {
                return null;
            }

            return new DetailedTrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToString("dd/MM/yyyy"),
                Specialties = trainer.Specialty.ToString(),
                Address = $"{trainer.Address.BuildingNo} - {trainer.Address.Street} - {trainer.Address.City}",
            };
        }

        public async Task<CreateTrainerViewModel> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<EditTrainerViewModel> EditTrainerAsync(EditTrainerViewModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
