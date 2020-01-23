using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<bool> SaveAsync(Author author);
        Task<bool> DeleteAsync(Author author);
    }
}
