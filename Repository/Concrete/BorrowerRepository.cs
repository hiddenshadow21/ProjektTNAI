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
    public class BorrowerRepository : BaseRepository, IBorrowerRepository
    {
        public async Task<Borrower> GetByIdAsync(int id)
        {
            return await context.Borrowers.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<Borrower>> GetAllAsync()
        {
            return await context.Borrowers.ToListAsync();
        }

        public async Task<bool> SaveAsync(Borrower borrower)
        {
            if (borrower == null)
                return false;
            try
            {
                context.Entry(borrower).State = (borrower.ID == default(int)) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Borrower borrower)
        {
            if (borrower == null)
                return false;
            context.Borrowers.Remove(borrower);
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
