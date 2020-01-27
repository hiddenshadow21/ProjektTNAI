using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models.OutputModels
{
    public class BookOutputModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Author { get; set; }
    }
}