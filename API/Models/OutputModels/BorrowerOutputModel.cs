using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels
{
    public class BorrowerOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}