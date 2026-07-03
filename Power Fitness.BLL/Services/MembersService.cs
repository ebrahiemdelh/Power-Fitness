using Power_Fitness.DAL.Contracts;

namespace Power_Fitness.BLL.Services
{
    public class MembersService : IMembersService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MembersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
        {
            var members = await _unitOfWork.Members.GetAllAsync(cancellationToken: cancellationToken);
            return members.Select(m => new MemberViewModel
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
            var member = await _unitOfWork.Members.GetByIdAsync(id, cancellationToken);
            var membershipPlan = await _unitOfWork.Members.GetPartialMemberShipDataByMemberIdAsync(id, cancellationToken);
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
                BuildingNo = member.Address.BuildingNo,
                Street= member.Address.Street,
                City= member.Address.City,
                PlanName = membershipPlan?.PlanName ?? "",
                MembershipStartDate = membershipPlan?.MembershipStartDate.ToString() ?? "",
                MembershipEndDate = membershipPlan?.MembershipEndDate.ToString() ?? "",
            };
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default)
        {
            var emailExists = await _unitOfWork.Members.EmailExistsAsync(member.Email, cancellationToken);
            var PhoneExists = await _unitOfWork.Members.PhoneExistsAsync(member.Phone, cancellationToken);

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
                    Note = member.HealthRecord.Note ?? "",
                },
                Address = new Address
                {
                    BuildingNo = member.BuildingNo,
                    Street = member.Street,
                    City = member.City,
                }
            };
            return (await _unitOfWork.Members.AddAsync(memberEntity, cancellationToken)) > 0;
        }

        public async Task<bool> UpdateMemberAsync(int id, EditMemberViewModel editMember, CancellationToken cancellationToken = default)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id, cancellationToken);
            if (member is null) return false;

            var emailExists = await _unitOfWork.Members.AnyAsync(m => m.Email == editMember.Email && m.Id != id, cancellationToken);
            var phoneExists = await _unitOfWork.Members.AnyAsync(m => m.Phone == editMember.Phone && m.Id != id, cancellationToken);
            if (emailExists || phoneExists) return false;

            member.Email = editMember.Email;
            member.Phone = editMember.Phone;
            member.Address.BuildingNo = editMember.BuildingNo;
            member.Address.Street = editMember.Street;
            member.Address.City = editMember.City;

            return (await _unitOfWork.Members.UpdateAsync(member, cancellationToken)) > 0;

        }


        public async Task<MemberHealthRecordViewModel?> GetHealthRecord(int memberId, CancellationToken cancellationToken = default)
        {
            var healthRecord = await _unitOfWork.HealthRecords.GetByMemberIdAsync(memberId, cancellationToken);
            if (healthRecord is null) return null;
            var healthRecordVM = new MemberHealthRecordViewModel
            {
                Height = healthRecord.Height,
                Weight = healthRecord.Weight,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Note ?? "",
            };
            return healthRecordVM;
        }

        public async Task<Membership?> GetMemberShipByMemberId(int memberId, CancellationToken cancellationToken = default!)
        {
            return await _unitOfWork.Members.GetMemberShipByMemberId(memberId, cancellationToken);
        }

        public async Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id, cancellationToken);
            if (member is null) return false;
            var hasBookings= await _unitOfWork.AnyAsync<Booking>(b => b.MemberId == id, cancellationToken);
            if (hasBookings) return false;
            return (await _unitOfWork.Members.DeleteAsync(member, cancellationToken)) > 0;
        }
    }
}
