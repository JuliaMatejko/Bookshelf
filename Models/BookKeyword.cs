using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Models
{
    public class BookKeyword
    {
        public int BookID { get; set; }
        public string KeywordID { get; set; }

        public Book Book { get; set; }
        public Keyword Keyword { get; set; }
    }
}
