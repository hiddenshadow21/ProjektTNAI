using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Configuration
{
    class BorrowerConfiguration : EntityTypeConfiguration<Borrower>
    {
        public BorrowerConfiguration()
        {
            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.FirstName).HasMaxLength(50);
            Property(x => x.LastName).HasMaxLength(50);
            HasMany(x => x.Loans).WithRequired(x => x.Borrower).HasForeignKey(x => x.BorrowerID);
        }
    }
}