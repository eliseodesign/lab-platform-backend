using LabPlatform.Models;
using LabPlatform.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPlatform
{
    public class ArticleRepository : IGenericRepository<Article>
    {
        private readonly DatabaseContext _db;

        public ArticleRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Article model)
        {
            // try
            // {
                _db.Articles.Add(model);
                await _db.SaveChangesAsync();
                return true;
            // }
            // catch
            // {
            //     return false;
            // }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Article Article = _db.Articles.First(a => a.Id == id);
                _db.Articles.Remove(Article);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IQueryable<Article>> GetAll()
        {
            IQueryable<Article> query = _db.Articles.Include((art)=> art.SystemUser);
            return query;
        }
        public async Task<Article> GetById(int id)
        {
            return await _db.Articles.FindAsync(id);
        }
        public async Task<bool> Update(Article model)
        {
            try
            {
                _db.Articles.Update(model);
                int affectedRows = await _db.SaveChangesAsync();

                // Verificar si al menos una fila se actualizó
                return affectedRows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
