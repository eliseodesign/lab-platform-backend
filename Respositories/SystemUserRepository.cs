using LabPlatform.Models;
using LabPlatform.Repositories;

namespace LabPlatform;

public class SystemUserRepository : IGenericRepository<SystemUser>
{
  private readonly DatabaseContext _db;

  public SystemUserRepository(DatabaseContext db)
  {
    _db = db;
  }
  public async Task<bool> Create(SystemUser model)
  {
    try
    {
      _db.Add(model);
      await _db.SaveChangesAsync();
      return true;
    }
    catch
    {
      // Manejo de errores
      return false;
    }
  }

  public async Task<bool> Update(SystemUser model)
  {
    try
    {
      _db.Update(model);
      await _db.SaveChangesAsync();
      return true;
    }
    catch
    {
      // Manejo de errores
      return false;
    }
  }

  public async Task<bool> Delete(int id)
  {
    var user = await _db.SystemUsers.FindAsync(id);
    if (user == null)
      return false;

    try
    {
      _db.Remove(user);
      await _db.SaveChangesAsync();
      return true;
    }
    catch
    {
      // Manejo de errores
      return false;
    }
  }

  public async Task<IQueryable<SystemUser>> GetAll()
  {
    return await Task.FromResult(_db.SystemUsers.AsQueryable());
  }

  public async Task<SystemUser?> GetById(int id)
  {
    return await _db.SystemUsers.FindAsync(id);
  }

}
