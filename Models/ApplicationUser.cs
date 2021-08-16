using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserBook> UserBooks { get; set; }
    }
}
