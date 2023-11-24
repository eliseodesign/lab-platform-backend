using LabPlatform.Models;
using LabPlatform.Repositories;

namespace LabPlatform;

public class ClientuserRepository : IGenericRepository<Clientuser>
{
  private readonly AuthdbContext _db;

  public ClientuserRepository(AuthdbContext db)
  {
    _db = db;
  }
  public async Task<bool> Create(Clientuser model)
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

  public async Task<bool> Update(Clientuser model)
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
    var user = await _db.Clientusers.FindAsync(id);
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

  public async Task<IQueryable<Clientuser>> GetAll()
  {
    return await Task.FromResult(_db.Clientusers.AsQueryable());
  }

  public async Task<Clientuser?> GetById(int id)
  {
    return await _db.Clientusers.FindAsync(id);
  }

}
