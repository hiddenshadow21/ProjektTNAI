using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.InputModels.Loan
{
    public class LoanInputModel
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int BorrowerID { get; set; }
        public DateTime LoanStart { get; set; }
    }
}