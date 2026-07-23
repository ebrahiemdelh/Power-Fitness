namespace Power_Fitness.BLL.Services
{
    public class MembersService : IMembersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
        {
            var members = await _unitOfWork.Members.GetAllAsync(cancellationToken: cancellationToken);
            
            return _mapper.Map<IEnumerable<MemberViewModel>>(members);
        }

        public async Task<DetailedMemberViewModel> GetMemberAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id, cancellationToken);
            var membershipPlan = await _unitOfWork.Members.GetPartialMemberShipDataByMemberIdAsync(id, cancellationToken);
            if (member == null) return null!;
            var memberDetails = _mapper.Map<DetailedMemberViewModel>(member);
            memberDetails.PlanName = membershipPlan?.PlanName ?? "No Plan";
            memberDetails.MembershipStartDate = membershipPlan?.MembershipStartDate.ToString() ?? "";
            memberDetails.MembershipEndDate = membershipPlan?.MembershipEndDate.ToString() ?? "";
            return memberDetails;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default)
        {
            var emailExists = await _unitOfWork.Members.EmailExistsAsync(member.Email, cancellationToken);
            var PhoneExists = await _unitOfWork.Members.PhoneExistsAsync(member.Phone, cancellationToken);

            if (emailExists || PhoneExists)
            {
                return false; // Email or Phone already exists
            }

            var memberEntity = _mapper.Map<Member>(member);
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
            
            var healthRecordVM = _mapper.Map<MemberHealthRecordViewModel>(healthRecord);
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
