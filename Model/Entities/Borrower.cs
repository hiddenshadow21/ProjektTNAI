using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Borrower
    {
        /// <summary>
        /// ID wypożyczającego
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Imie wypożyczającego
        /// </summary>
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        /// <summary>
        /// Nazwisko wypożyczającego
        /// </summary>
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        /// <summary>
        /// Aktywne wypożyczenia
        /// </summary>
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
