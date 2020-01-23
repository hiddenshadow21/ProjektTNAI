using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels
{
    public class LoanOutputModel
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Book { get; set; }
        public int BorrowerID { get; set; }
        public string Borrower { get; set; }
        public DateTime LoanStart { get; set; }
    }
}