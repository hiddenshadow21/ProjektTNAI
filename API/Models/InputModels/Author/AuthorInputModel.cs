using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models.InputModels
{
    public class AuthorInputModel
    {
        /// <summary>
        /// ID autora
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// Imię autora
        /// </summary>
        [Required]
        [MaxLength(length: 50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko autora
        /// </summary>
        [Required]
        [MaxLength(length: 50)]
        public string LastName { get; set; }
    }
}