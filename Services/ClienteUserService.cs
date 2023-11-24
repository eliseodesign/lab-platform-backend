using LabPlatform.Models;
using LabPlatform.Repositories;
using LabPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabPlatform.Services;

public class ClienteUserService : IClienteUserService
{

    private readonly IGenericRepository<Clientuser> _userRepo;
    public ClienteUserService(IGenericRepository<Clientuser> userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> Create(Clientuser model) => await _userRepo.Create(model);

    public async Task<bool> Update(Clientuser model) => await _userRepo.Update(model);
    public async Task<bool> Delete(int id) => await _userRepo.Delete(id);

    public Task<IQueryable<Clientuser>> GetAll() => _userRepo.GetAll();
    public async Task<bool> ConfirmAccount(string token)
    {
        try
        {
            var users = await _userRepo.GetAll();
            Clientuser existingClientuser = await users.FirstAsync(a => a.Token == token);
            existingClientuser.Confirmaccount = true;

            await _userRepo.Update(existingClientuser);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Clientuser?> GetByEmail(string email)
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

    public Task<Clientuser?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RestartAccount(bool restart, string password, string token)
    {
        throw new NotImplementedException();
    }


    public async Task<Clientuser?> Validate(string email, string password)
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
            var result = await users.FirstAsync(a => a.Email == email && a.Confirmaccount == true);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
