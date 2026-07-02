using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class MembersService : IMembersService
    {
        private readonly IMemberRepository _membersRepository;
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MembersService(IMemberRepository membersRepository,
            IPlanRepository planRepository,
            IUnitOfWork unitOfWork)
        {
            _membersRepository = membersRepository;
            _planRepository = planRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SessionViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
        {
            var members = await _membersRepository.GetAllAsync(cancellationToken: cancellationToken);
            return members.Select(m => new SessionViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Gender = m.Gender.ToString(),
                Phone = m.Phone,
                Photo = m.Photo,

            });
        }

        public async Task<DetailedMemberViewModel> GetMemberAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await _membersRepository.GetByIdAsync(id, cancellationToken);
            var membership = await _membersRepository.GetMemberShipByMemberId(id, cancellationToken);
            var plan = await _planRepository.GetByMemberIdAsync(id, cancellationToken);
            if (member == null) return null!;
            return new DetailedMemberViewModel
            {
                Id = id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DOB = member.DateOfBirth.ToString("yyyy/MM/dd"),
                Gender = member.Gender.ToString(),
                Photo = member.Photo,
                Address = $"{member.Address.BuildingNo} - {member.Address.Street} - {member.Address.City}",
                PlanName = plan?.Name ?? "",
                MembershipStartDate = membership?.CreatedAt.ToString() ?? "",
                MembershipEndDate = membership?.EndDate.ToString() ?? "",
            };
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default)
        {
            var emailExists = await _membersRepository.EmailExistsAsync(member.Email, cancellationToken);
            var PhoneExists = await _membersRepository.PhoneExistsAsync(member.Phone, cancellationToken);

            if (emailExists || PhoneExists)
            {
                return false; // Email or Phone already exists
            }

            var memberEntity = new Member
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DOB,
                Gender = member.Gender,
                Photo = "",

                HealthRecord = new HealthRecord
                {
                    BloodType = member.HealthRecord.BloodType,
                    Height = member.HealthRecord.Height,
                    Weight = member.HealthRecord.Weight,
                    Note = member.HealthRecord.Note,
                },
                Address = new Address
                {
                    BuildingNo = member.BuildingNo,
                    Street = member.Street,
                    City = member.City,
                }
            };
            return (await _membersRepository.AddAsync(memberEntity, cancellationToken)) > 0;
        }

        public async Task<bool> UpdateMemberAsync(int id, EditMemberViewModel editMember, CancellationToken cancellationToken = default)
        {
            var member = await _membersRepository.GetByIdAsync(id, cancellationToken);
            if (member is null) return false;

            var emailExists = await _membersRepository.AnyAsync(m => m.Email == editMember.Email && m.Id != id, cancellationToken);
            var phoneExists = await _membersRepository.AnyAsync(m => m.Phone == editMember.Phone && m.Id != id, cancellationToken);
            if (emailExists || phoneExists) return false;

            member.Email = editMember.Email;
            member.Phone = editMember.Phone;
            member.Address.BuildingNo = editMember.BuildingNo;
            member.Address.Street = editMember.Street;
            member.Address.City = editMember.City;

            return (await _membersRepository.UpdateAsync(member, cancellationToken)) > 0;

        }


        public async Task<MemberHealthRecordViewModel?> GetHealthRecord(int memberId, CancellationToken cancellationToken = default)
        {
            var healthRecord = await _healthRecordRepository.GetByMemberIdAsync(memberId, cancellationToken);
            if (healthRecord is null) return null;
            var healthRecordVM = new MemberHealthRecordViewModel
            {
                Height = healthRecord.Height,
                Weight = healthRecord.Weight,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Note,
            };
            return healthRecordVM;
        }

        public async Task<Membership?> GetMemberShipByMemberId(int memberId, CancellationToken cancellationToken = default!)
        {
            return await _membersRepository.GetMemberShipByMemberId(memberId, cancellationToken);
        }
    }
}
