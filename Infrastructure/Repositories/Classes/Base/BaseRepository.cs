using Infrastructure.Repositories.Interfaces.Base;

namespace Infrastructure.Repositories.Classes.Base;

public class BaseRepository : IBaseRepository {
    
    public Task<T> GetByIdAsync<T>(Guid id) where T : class {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync<T>() where T : class {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync<T>(T entity) where T : class {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync<T>(T entity) where T : class {
        throw new NotImplementedException();
    }

    public Task<T> DeleteAsync<T>(Guid id) where T : class {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync() {
        throw new NotImplementedException();
    }
}