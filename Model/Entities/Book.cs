using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
