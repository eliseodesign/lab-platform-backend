using LabPlatform.Models;
using LabPlatform.Repositories;
using LabPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabPlatform.Services;

public class SystemUserService : ISystemUserService
{

    private readonly IGenericRepository<SystemUser> _userRepo;
    public SystemUserService(IGenericRepository<SystemUser> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> Create(SystemUser model) => await _userRepo.Create(model);

    public async Task<bool> Update(SystemUser model) => await _userRepo.Update(model);
    public async Task<bool> Delete(int id) => await _userRepo.Delete(id);

    public Task<IQueryable<SystemUser>> GetAll() => _userRepo.GetAll();
    public async Task<bool> ConfirmAccount(string token)
    {
        try
        {
            var users = await _userRepo.GetAll();
            SystemUser existingSystemUser = await users.FirstAsync(a => a.Token == token);
            existingSystemUser.ConfirmAccount = true;

            await _userRepo.Update(existingSystemUser);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<SystemUser?> GetByEmail(string email)
    {
        try
        {
            var users = await _userRepo.GetAll();
            var result = await users.FirstAsync(a => a.Email == email);
            return result;
        }
        catch
        {
            return null;
        }
    }

    public Task<SystemUser?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RestartAccount(bool restart, string password, string token)
    {
        throw new NotImplementedException();
    }


    public async Task<SystemUser?> Validate(string email, string password)
    {
        try
        {
            var users = await _userRepo.GetAll();
            var result = await users.FirstAsync(a => a.Email == email && a.Password == password);
            return result;
        }
        catch
        {
            return null;
        }
    }

     public async Task<bool> ValidateConfirm(string email)
    {
        try
        {
            var users = await _userRepo.GetAll();
            var result = await users.FirstAsync(a => a.Email == email && a.ConfirmAccount == true);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
