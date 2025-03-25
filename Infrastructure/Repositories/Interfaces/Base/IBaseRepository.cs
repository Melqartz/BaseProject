namespace Infrastructure.Repositories.Interfaces.Base;

public interface IBaseRepository {
    Task<T> GetByIdAsync<T>(Guid id) where T : class;
    Task<List<T>> GetAllAsync<T>() where T : class;
    Task<T> AddAsync<T>(T entity) where T : class;
    Task<T> UpdateAsync<T>(T entity) where T : class;
    Task<T> DeleteAsync<T>(Guid id) where T : class;
    Task<bool> SaveChangesAsync();
    
}