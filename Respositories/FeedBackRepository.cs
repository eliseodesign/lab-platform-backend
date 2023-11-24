using LabPlatform.Models;
using LabPlatform.Repositories;

namespace LabPlatform;

public class FeedbackRepository : IGenericRepository<Feedback>
{
  private readonly AuthdbContext _db;
  public FeedbackRepository(AuthdbContext db)
  {
    _db = db;
  }

   public async Task<bool> Create(Feedback model)
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

  public async Task<bool> Update(Feedback model)
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
    var user = await _db.Feedbacks.FindAsync(id);
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

  public async Task<IQueryable<Feedback>> GetAll()
  {
    return await Task.FromResult(_db.Feedbacks.AsQueryable());
  }

  public async Task<Feedback?> GetById(int id)
  {
    return await _db.Feedbacks.FindAsync(id);
  }
}
