using Power_Fitness.BLL.ViewModels.Session;
using Power_Fitness.BLL.ViewModels.Trainer;

namespace Power_Fitness.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MemberMapping();
            TrainerMapping();
            SessionMapping();
            CreateMap<HealthRecord, MemberHealthRecordViewModel>();
        }

        private void MemberMapping()
        {
            CreateMap<Member, MemberViewModel>();

            CreateMap<Member, DetailedMemberViewModel>()
                .ForMember(d => d.DOB, o => o.MapFrom(s => s.DateOfBirth.ToString()))
                .ForMember(d => d.BuildingNo, o => o.MapFrom(s => s.Address.BuildingNo))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(d => d.Address, options => options.MapFrom(s => new Address
                {
                    BuildingNo = s.BuildingNo,
                    Street = s.Street,
                    City = s.City
                }));
            CreateMap<HealthRecordViewModel, HealthRecord>();

        }
        private void TrainerMapping()
        {
            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(d=>d.Specialties,o=>o.MapFrom(s=>s.Specialty.ToString()));

            CreateMap<Trainer, DetailedTrainerViewModel>()
                .ForMember(d=>d.Specialties,o=>o.MapFrom(s=>s.Specialty.ToString()))
                .ForMember(d => d.Address, o => o.MapFrom(s => $"{s.Address.BuildingNo} - {s.Address.Street} - {s.Address.City}"));

            CreateMap<Trainer, EditTrainerViewModel>()
                .ForMember(d=>d.Specialties,o=>o.MapFrom(s=>s.Specialty.ToString()))
                .ForMember(d => d.BuildingNumber, o => o.MapFrom(s => s.Address.BuildingNo))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City));

            CreateMap<CreateTrainerViewModel,Trainer>()
                .ForMember(d=>d.Specialty,o=>o.MapFrom(s=>s.Specialties))
                .ForMember(d=>d.Address, o => o.MapFrom(s => new Address
                {
                    BuildingNo = s.BuildingNumber,
                    Street = s.Street,
                    City = s.City
                }));
        }
        private void SessionMapping()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(d=>d.CategoryName,o=>o.MapFrom(s=>s.Category.Name))
                .ForMember(d=>d.TrainerName,o=>o.MapFrom(s=>s.Trainer.Name));
            
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, EditSessionViewModel>();
        }
    }
}
