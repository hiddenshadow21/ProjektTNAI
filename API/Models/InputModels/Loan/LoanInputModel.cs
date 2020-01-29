using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.InputModels.Loan
{
    public class LoanInputModel
    {
        /// <summary>
        /// ID wypożyczenia
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID książki
        /// </summary>
        public int BookID { get; set; }
        /// <summary>
        /// ID wypożyczającego
        /// </summary>
        public int BorrowerID { get; set; }
        /// <summary>
        /// Data rozpoczęcia wypożyczania
        /// </summary>
        public DateTime LoanStart { get; set; }
    }
}