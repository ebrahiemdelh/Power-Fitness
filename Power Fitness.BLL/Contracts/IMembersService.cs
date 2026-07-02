namespace Power_Fitness.BLL.Contracts
{
    public interface IMembersService
    {
        Task<IEnumerable<SessionViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default);

        Task<DetailedMemberViewModel> GetMemberAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default);
        Task<bool> UpdateMemberAsync(int id, EditMemberViewModel member, CancellationToken cancellationToken = default);



        Task<Membership?> GetMemberShipByMemberId(int memberId, CancellationToken cancellationToken = default);
        Task<MemberHealthRecordViewModel?> GetHealthRecord(int memberId, CancellationToken cancellationToken = default);
    }
}
