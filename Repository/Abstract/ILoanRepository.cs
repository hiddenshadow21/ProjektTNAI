using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(int id);
        Task<List<Loan>> GetAllAsync();
        Task<bool> SaveAsync(Loan loan);
        Task<bool> DeleteAsync(Loan loan);
    }
}
