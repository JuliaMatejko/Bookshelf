using System;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public enum BookStatus
    {
        WantToRead = 0,
        ReadingRightNow = 1,
        Read = 2
    }

    public class UserBook
    {
        public string ApplicationUserID { get; set; }
        public int BookID { get; set; }
        [Range(1,10)]
        [DisplayFormat(NullDisplayText = "Not rated yet")]
        public int? Rating { get; set; }
#nullable enable
        [DisplayFormat(NullDisplayText = "No review")]
        public string? Review { get; set; }
#nullable disable
        public BookStatus BookStatus { get; set; }

        public Book Book { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
