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
    public class LoanRepository : BaseRepository, ILoanRepository
    {
        public async Task<Loan> GetByIdAsync(int id)
        {
            return await context.Loans.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await context.Loans.ToListAsync();
        }

        public async Task<bool> SaveAsync(Loan loan)
        {
            if (loan == null)
                return false;
            try
            {
                context.Entry(loan).State = (loan.ID == default(int)) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Loan loan)
        {
            if (loan == null)
                return false;
            context.Loans.Remove(loan);
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
