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
        public int ID { get; set; }
        [Display(Name = "Tytuł")]
        public string Name { get; set; }
        [Display(Name = "Ilość")]
        public int Amount { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
