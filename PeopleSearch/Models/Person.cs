using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PeopleSearch.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // age should be calculated rather than stored
        [NotMapped]
        public int Age =>
            (int)(DateTime.Now - BirthDate).TotalDays / 365;

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        // let photo be optional
        public string PhotoFile { get; set; }

        [NotMapped]
        public virtual IFormFile Photo { get; set; }

        public virtual IEnumerable<Interest> Interests { get; set; }

        [NotMapped]
        public string InterestsAsString =>
            string.Join(", ", Interests.Select(x => x.Name));
    }
}
