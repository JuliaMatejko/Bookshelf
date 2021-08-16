using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Added On")]
        public DateTime AddedOn { get; set; }
#nullable enable
        [DisplayFormat(NullDisplayText = "No description")]
        [StringLength(5000)]
        public string? Description { get; set; }
#nullable disable

        public ICollection<AuthorBook> AuthorsBooks { get; set; }
        public ICollection<BookKeyword> BooksKeywords { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }

    }
}
