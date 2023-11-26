using LabPlatform.Models;
using LabPlatform.Models.DTOs;
using LabPlatform.Repositories;
using LabPlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;

namespace LabPlatform.Services;

public class ArticleService : IArticleService
{

    private readonly IGenericRepository<Article> repo;
    public ArticleService(IGenericRepository<Article> userRepo)
    {
        repo = userRepo;
    }
    public async Task<bool> Create(Article model)
    {
        return await repo.Create(model);
    }

    public Task<bool> Delete(int id) => repo.Delete(id);

    public async Task<List<ArticleDTO>> GetAll()
    {
        var query = await repo.GetAll();

        List<ArticleDTO> list = new List<ArticleDTO>();

        foreach (var article in query)
        {
            ArticleDTO articleDTO = TinyMapper.Map<ArticleDTO>(article);
            list.Add(articleDTO);
        }

        return list;
    }

    public async Task<ArticleDTO?> GetById(int id) => TinyMapper.Map<ArticleDTO>(await repo.GetById(id));


    public Task<bool> Update(Article model) => repo.Update(model);
}
