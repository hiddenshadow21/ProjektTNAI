using Model.Configuration;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public AppDbContext() : base("AppDbConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new BorrowerConfiguration());
            modelBuilder.Configurations.Add(new LoanConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
