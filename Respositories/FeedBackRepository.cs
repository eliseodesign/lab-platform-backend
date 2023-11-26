using LabPlatform.Models;
using LabPlatform.Repositories;

namespace LabPlatform;

public class FeedbackRepository : IGenericRepository<FeedBack>
{
  private readonly DatabaseContext _db;
  public FeedbackRepository(DatabaseContext db)
  {
    _db = db;
  }

   public async Task<bool> Create(FeedBack model)
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

  public async Task<bool> Update(FeedBack model)
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
    var user = await _db.FeedBacks.FindAsync(id);
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

  public async Task<IQueryable<FeedBack>> GetAll()
  {
    return await Task.FromResult(_db.FeedBacks.AsQueryable());
  }

  public async Task<FeedBack?> GetById(int id)
  {
    return await _db.FeedBacks.FindAsync(id);
  }
}
