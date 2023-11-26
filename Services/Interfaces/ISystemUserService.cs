using LabPlatform.Models;

namespace LabPlatform.Services.Interfaces;

public interface ISystemUserService
{
  Task<bool> Create(SystemUser model);
  Task<bool> Update(SystemUser model);
  Task<bool> Delete(int id);
  Task<IQueryable<SystemUser>> GetAll();
  Task<SystemUser?> GetById(int id);
  Task<SystemUser?> Validate(string email, string password);
  Task<bool> ValidateConfirm(string email);
  Task<SystemUser?> GetByEmail(string email);
  Task<bool> RestartAccount(bool restart, string password, string token);
  Task<bool> ConfirmAccount(string token);
}
