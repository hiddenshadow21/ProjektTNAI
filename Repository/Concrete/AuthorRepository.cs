using Model.Entities;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public async Task<Author> GetByIdAsync(int id)
        {
            return await context.Authors.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<bool> SaveAsync(Author author)
        {
            if (author == null)
                return false;
            try
            {
                context.Entry(author).State = (author.ID == default(int)) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Author author)
        {
            if (author == null)
                return false;
            context.Authors.Remove(author);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
