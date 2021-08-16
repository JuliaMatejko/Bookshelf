using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
#nullable enable
        [StringLength(50)]
        [Display(Name = "Second Name")]
        public string? SecondName { get; set; }
#nullable disable
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                if (SecondName == null)
                {
                    return $"{FirstName} {LastName}";
                }
                return $"{FirstName} {SecondName} {LastName}";
            }
        }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}