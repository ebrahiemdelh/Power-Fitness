namespace Power_Fitness.BLL.Contracts
{
    public interface IMembersService
    {
        Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default);

        Task<DetailedMemberViewModel> GetMemberAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default);
        Task<bool> UpdateMemberAsync(int id, EditMemberViewModel member, CancellationToken cancellationToken = default);
    }
}
