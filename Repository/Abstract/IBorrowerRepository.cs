using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IBorrowerRepository
    {
        Task<Borrower> GetByIdAsync(int id);
        Task<List<Borrower>> GetAllAsync();
        Task<bool> SaveAsync(Borrower borrower);
        Task<bool> DeleteAsync(Borrower borrower);
    }
}
