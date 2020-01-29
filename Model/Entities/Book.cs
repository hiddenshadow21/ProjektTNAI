using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Book
    {
        /// <summary>
        /// ID książki
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Tytuł książki
        /// </summary>
        [Display(Name = "Tytuł")]
        public string Name { get; set; }
        /// <summary>
        /// Ilośc kopii danej książki
        /// </summary>
        [Display(Name = "Ilość")]
        public int Amount { get; set; }
        /// <summary>
        /// ID autora
        /// </summary>
        public int AuthorID { get; set; }
        /// <summary>
        /// Dane autora
        /// </summary>
        public virtual Author Author { get; set; }
    }
}
