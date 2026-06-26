namespace Power_Fitness.DAL.Repositories
{
    public class MockRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly List<T> _entities;
        public MockRepository()
        {
            _entities = new List<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_entities);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_entities.FirstOrDefault(e => e.Id == id));
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            _entities.Add(entity);
            return 1;
        }

        public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var index = _entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _entities[index] = entity;
            }
            return 1;
        }

        public async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _entities.Remove(entity);
            return 1;
        }
    }
}
