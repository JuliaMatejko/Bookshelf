using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Keyword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Keyword")]
        [Display(Name = "Keyword")]
        [StringLength(50, MinimumLength=2)]
        public string KeywordID { get; set; }

        public ICollection<BookKeyword> BookKeyword { get; set; }

    }
}