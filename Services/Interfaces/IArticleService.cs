using LabPlatform.Models;
using LabPlatform.Models.DTOs;
namespace LabPlatform;

public interface IArticleService
    {
        Task<bool> Create(Article model);
        Task<bool> Update(Article model);
        Task<bool> Delete(int id);
        Task<List<ArticleDTO>> GetAll();
        Task<ArticleDTO?> GetById(int id);
    }
