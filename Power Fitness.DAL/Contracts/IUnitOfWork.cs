namespace Power_Fitness.DAL.Contracts
{
    public interface IUnitOfWork
    {
        //public IHealthRecordRepository HealthRecordRepository { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
