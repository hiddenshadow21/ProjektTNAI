using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<bool> SaveAsync(Book book);
        Task<bool> DeleteAsync(Book book);
    }
}
