using Bookshelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Kewords.Any())
            {
                return;
            }

            var keywords = new Keyword[]
            {
                new Keyword{KeywordID = "fiction"},
                new Keyword{KeywordID = "mystery"},
                new Keyword{KeywordID = "detective"},
                new Keyword{KeywordID = "science-fiction"},
                new Keyword{KeywordID = "sf"},
                new Keyword{KeywordID = "apocalyptic"},
                new Keyword{KeywordID = "post-apocalyptic"},
                new Keyword{KeywordID = "dystopia"},
                new Keyword{KeywordID = "horror"},
                new Keyword{KeywordID = "alien"},
                new Keyword{KeywordID = "planet"}
            };
            foreach (Keyword k in keywords)
            {
                context.Kewords.Add(k);
            }
            context.SaveChanges();

            var authors = new Author[]
            {
                new Author{FirstName = "Stanisław", LastName = "Lem"},
                new Author{FirstName = "Dmitry", LastName = "Glukhovsky"},
                new Author{FirstName = "Liu", LastName = "Cixin"},
                new Author{FirstName = "Arthur", SecondName="Conan", LastName = "Doyle"},
                new Author{FirstName = "Frank", LastName = "Herbert"}
            };
            foreach (Author a in authors)
            {
                context.Authors.Add(a);
            }
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{Title = "Solaris", AddedOn = DateTime.Parse("29-07-2021"), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam elementum ligula sit amet felis vulputate, vitae sollicitudin ligula sagittis. Etiam in neque id sapien facilisis consequat. Donec varius diam nec mi condimentum ullamcorper. Donec eu purus quis tortor condimentum aliquet. Nulla at faucibus dolor, fermentum fermentum diam. Cras fermentum scelerisque tempor. Fusce ac metus volutpat, mollis nulla non, dapibus quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aliquam erat volutpat."},
                new Book{Title = "Metro 2033", AddedOn = DateTime.Parse("31-07-2021"), Description = "Nulla sagittis nisl sed tortor ultrices, a mattis nunc tincidunt. Donec dictum, dolor ac laoreet viverra, mi mi pellentesque purus, eu fringilla ex sapien et arcu. Vivamus finibus metus eros. In at quam quis eros eleifend condimentum. Quisque pretium fringilla leo a mattis. Proin ut faucibus neque. Aliquam iaculis enim at mauris porttitor, sit amet molestie arcu condimentum. Suspendisse lobortis, mauris vel porta rhoncus, lectus mi efficitur justo, vel euismod tortor massa a nunc. Proin et mattis lacus. In fringilla ultrices posuere. Integer tempus mauris turpis, id fringilla tellus hendrerit id."},
                new Book{Title = "FUTU.RE", AddedOn = DateTime.Parse("01-08-2021"), Description = "Nullam in dapibus arcu, sed posuere arcu. Curabitur varius tempus enim eu laoreet. Vivamus facilisis justo vitae risus sollicitudin, semper viverra nisi viverra. Vestibulum facilisis orci eu mi vehicula, ut suscipit augue finibus. Fusce eget nulla enim. Aliquam vitae risus in dolor condimentum ornare eget vel quam. Fusce posuere leo odio, eget vulputate metus sodales eu. Vestibulum tempus nunc felis. Ut lorem nisi, varius id dictum sit amet, consectetur eget metus. Donec placerat lacinia enim, ut lobortis urna molestie at. Nam ac sagittis lectus. Cras id ipsum a justo placerat mattis sit amet et diam. Quisque commodo est semper, aliquet turpis placerat, rutrum enim. Fusce dapibus tristique suscipit."},
                new Book{Title = "The Three-Body Problem", AddedOn = DateTime.Parse("03-08-2021"), Description = "Nam dictum, lorem quis scelerisque porta, tellus purus mollis velit, tempor venenatis orci ex sed lacus. Sed mattis elementum dolor eu sodales. Praesent eu lacus nec tortor interdum tincidunt nec nec dolor. Ut vel metus vel nibh tincidunt imperdiet. Aliquam tincidunt blandit sapien quis ultricies. Duis laoreet ex tortor, mattis tempus mi tincidunt ut. In accumsan orci eros, in laoreet nisl rutrum ut. Mauris tempor ac nisl ac fringilla. Aliquam dictum tortor ac aliquam pellentesque. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae;"},
                new Book{Title = "The Dark Forest", AddedOn = DateTime.Parse("03-08-2021"), Description = "Sed ac nulla eget magna laoreet tristique ac a eros. Suspendisse eleifend pharetra tortor id posuere. Maecenas aliquet, elit eu iaculis vestibulum, mi urna tincidunt nibh, sed convallis metus augue id arcu. Fusce ut pellentesque felis. Ut consequat hendrerit erat, sed bibendum est lacinia ut. Nulla varius magna quis aliquet pretium. Aenean porta sed ipsum suscipit luctus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Proin ut dui eu nulla scelerisque tempor. Proin sem quam, tincidunt eget velit maximus, vulputate fermentum urna. Sed vitae elit tempus, eleifend nisl id, cursus mauris. Integer porta hendrerit arcu sit amet mattis. Ut non finibus mauris, et bibendum dui."},
                new Book{Title = "Death's End", AddedOn = DateTime.Parse("04-08-2021"), Description = "Phasellus blandit augue non mi ultricies tincidunt. Morbi sit amet mauris sagittis, mollis enim eget, venenatis nisl. Aliquam mi ipsum, finibus in iaculis sit amet, tristique vitae nisi. Aliquam condimentum, felis tempor aliquam imperdiet, nibh erat mattis mi, ac rhoncus nisl quam eget nisi. Fusce a congue purus. Vestibulum scelerisque sed risus a ornare. Nam id erat sit amet nunc pulvinar placerat.Mauris ut iaculis metus. Cras pellentesque ante id arcu facilisis, vitae rutrum diam consequat. Nam maximus viverra est. In viverra maximus nibh sed scelerisque. Maecenas lobortis ipsum a velit commodo, nec lobortis justo consequat. Nam lorem dui, vestibulum vitae metus eu, consectetur auctor nisi. Donec tempus urna id arcu tristique molestie."},
                new Book{Title = "A Study in Scarlet", AddedOn = DateTime.Parse("11-08-2021"), Description = " Praesent ac purus consequat, porttitor mi eu, vestibulum ex. Vivamus tincidunt vulputate leo, imperdiet commodo tellus cursus id. Vestibulum ut venenatis lectus. Maecenas mattis pretium nisl, eu rutrum orci molestie eget. Etiam sit amet lectus pulvinar, semper massa nec, ullamcorper augue. Etiam lacus metus, varius at magna rhoncus, semper sagittis magna. Phasellus facilisis ultricies quam porttitor mattis. Cras rutrum tellus non laoreet vestibulum. Nam nec neque justo. Nulla ultrices erat enim, ut vestibulum purus porta quis. Sed porta, nisl id ultrices iaculis, metus massa dictum nisi, a congue lectus nisl id turpis. Integer ligula mauris, facilisis ac accumsan ac, facilisis ac erat."},
                new Book{Title = "The Call of Cthulhu", AddedOn = DateTime.Parse("14-08-2021"), Description = "Sed sem urna, sagittis in efficitur eget, malesuada at lacus. Praesent ut leo lobortis, varius libero in, lobortis justo. In a enim viverra, placerat diam et, tincidunt turpis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Pellentesque vulputate eu nisi sed placerat. Duis vulputate justo eu quam interdum, at tempus diam dapibus. Sed at viverra nibh. Maecenas sit amet faucibus leo. Praesent tellus odio, rhoncus vitae leo at, porta suscipit elit. Praesent vitae sapien mauris. Sed viverra sollicitudin mollis. Integer lobortis dolor dolor, at efficitur libero lacinia ac. Mauris eros nulla, malesuada id mollis eu, gravida eget arcu. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed posuere elementum enim pharetra placerat."},
                new Book{Title = "Dune", AddedOn = DateTime.Parse("23-08-2021"), Description = "Donec dapibus turpis massa, sed placerat purus porta eu. Ut id ante massa. Phasellus dui tortor, blandit et neque eu, luctus tincidunt nisl. Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent eget commodo leo, vitae egestas nisl. Cras ac velit enim. Proin id orci id leo tristique egestas non quis sem. Nam molestie erat ut sapien tempus, vel mattis justo tempor. Mauris justo lacus, tristique nec viverra et, malesuada non justo. Aenean congue mi id facilisis placerat."}
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();

            var authorsbooks = new AuthorBook[]
            {
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Lem").AuthorID, 
                    BookID = books.Single(b => b.Title == "Solaris").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Glukhovsky").AuthorID,
                    BookID = books.Single(b => b.Title == "Metro 2033").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Glukhovsky").AuthorID,
                    BookID = books.Single(b => b.Title == "FUTU.RE").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Cixin").AuthorID,
                    BookID = books.Single(b => b.Title == "The Three-Body Problem").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Cixin").AuthorID,
                    BookID = books.Single(b => b.Title == "The Dark Forest").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Cixin").AuthorID,
                    BookID = books.Single(b => b.Title == "Death's End").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Doyle").AuthorID,
                    BookID = books.Single(b => b.Title == "A Study in Scarlet").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Lovecraft").AuthorID,
                    BookID = books.Single(b => b.Title == "The Call of Cthulhu").BookID
                },
                new AuthorBook{
                    AuthorID = authors.Single(a => a.LastName == "Herbert").AuthorID,
                    BookID = books.Single(b => b.Title == "Dune").BookID
                },
            };
            foreach (AuthorBook ab in authorsbooks)
            {
                var authorbooktInDataBase = context.AuthorsBooks.Where(
                   s =>
                            s.Author.AuthorID == ab.AuthorID &&
                            s.Book.BookID == ab.BookID).SingleOrDefault();
                if (authorbooktInDataBase == null)
                {
                    context.AuthorsBooks.Add(ab);
                }
            }
            context.SaveChanges();

            var bookskeywords = new BookKeyword[]
            {
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Solaris").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Solaris").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Solaris").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "alien").KeywordID
                },
                 new BookKeyword{
                    BookID = books.Single(b => b.Title == "Solaris").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "planet").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "The Three-Body Problem").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "The Three-Body Problem").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "The Dark Forest").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "The Dark Forest").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Death's End").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Death's End").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "FUTU.RE").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "FUTU.RE").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Metro 2033").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "apocalyptic").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Metro 2033").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "post-apocalyptic").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Metro 2033").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "dystopia").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Metro 2033").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "horror").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "The Call of Cthulhu").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "horror").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "A Study in Scarlet").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "A Study in Scarlet").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "mystery").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "A Study in Scarlet").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "detective").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Dune").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "sf").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Dune").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "science-fiction").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Dune").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "planet").KeywordID
                },
            };
            foreach (BookKeyword bk in bookskeywords)
            {
                var bookskeywordsInDataBase = context.BooksKeywords.Where(
                   s =>
                            s.Book.BookID == bk.BookID &&
                            s.Keyword.KeywordID == bk.KeywordID).SingleOrDefault();
                if (bookskeywordsInDataBase == null)
                {
                    context.BooksKeywords.Add(bk);
                }
            }
            context.SaveChanges();
        }
    }
}
