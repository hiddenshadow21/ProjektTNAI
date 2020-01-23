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
    public class BookRepository : BaseRepository, IBookRepository
    {
        public async Task<Book> GetByIdAsync(int id)
        {
            return await context.Books.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<bool> SaveAsync(Book book)
        {
            if (book == null)
                return false;
            try
            {
                context.Entry(book).State = (book.ID == default(int)) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            if (book == null)
                return false;
            context.Books.Remove(book);
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
