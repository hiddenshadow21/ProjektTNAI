using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Loan
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
        public int BorrowerID { get; set; }
        public virtual Borrower Borrower { get; set; }
        public DateTime LoanStart { get; set; }
    }
}
