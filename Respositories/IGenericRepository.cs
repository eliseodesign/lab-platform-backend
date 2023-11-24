namespace LabPlatform.Repositories;

public interface IGenericRepository<T> where T : class
    {
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
        Task<IQueryable<T>> GetAll();
        Task<T?> GetById(int id);
    }