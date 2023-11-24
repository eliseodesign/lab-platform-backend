using LabPlatform.Models;

namespace LabPlatform.Services.Interfaces;

public interface IClienteUserService
{
  Task<bool> Create(Clientuser model);
  Task<bool> Update(Clientuser model);
  Task<bool> Delete(int id);
  Task<IQueryable<Clientuser>> GetAll();
  Task<Clientuser?> GetById(int id);
  Task<Clientuser?> Validate(string email, string password);
  Task<bool> ValidateConfirm(string email);
  Task<Clientuser?> GetByEmail(string email);
  Task<bool> RestartAccount(bool restart, string password, string token);
  Task<bool> ConfirmAccount(string token);
}
