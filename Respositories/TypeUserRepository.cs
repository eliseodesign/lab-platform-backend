using LabPlatform.Models;
using LabPlatform.Repositories;

namespace LabPlatform;

public class TypeuserRepository : IGenericRepository<Typeuser>
{
  private readonly AuthdbContext _db;
  public TypeuserRepository(AuthdbContext db){
    _db = db;
  }
  public Task<bool> Create(Typeuser model)
  {
    throw new NotImplementedException();
  }

  public Task<bool> Delete(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<IQueryable<Typeuser>> GetAll()
  {
    return await Task.FromResult(_db.Typeusers.AsQueryable());
  }

  public Task<Typeuser?> GetById(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> Update(Typeuser model)
  {
    throw new NotImplementedException();
  }
}
