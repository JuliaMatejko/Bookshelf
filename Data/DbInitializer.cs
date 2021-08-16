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
                new Keyword{KeywordID = "artificial intelligence"},
                new Keyword{KeywordID = "ai"}
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
                new Author{FirstName = "Howard", SecondName="Phillips", LastName = "Lovecraft"}
            };
            foreach (Author a in authors)
            {
                context.Authors.Add(a);
            }
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{Title = "Solaris", AddedOn =  DateTime.Parse("29-07-2021"), Description = "book decription1"},
                new Book{Title = "Metro 2033", AddedOn =  DateTime.Parse("31-07-2021"), Description = "book decription2"},
                new Book{Title = "FUTU.RE", AddedOn =  DateTime.Parse("01-08-2021"), Description = "book decription3"},
                new Book{Title = "The Three-Body Problem", AddedOn =  DateTime.Parse("03-08-2021"), Description = "book decription4"},
                new Book{Title = "The Dark Forest", AddedOn =  DateTime.Parse("03-08-2021"), Description = "book decription5"},
                new Book{Title = "Death's End", AddedOn =  DateTime.Parse("04-08-2021"), Description = "book decription6"},
                new Book{Title = "A Study in Scarlet", AddedOn =  DateTime.Parse("11-08-2021"), Description = "book decription7"},
                new Book{Title = "The Call of Cthulhu", AddedOn =  DateTime.Parse("14-08-2021"), Description = "book decription8"}
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
                    KeywordID = keywords.Single(k => k.KeywordID == "ai").KeywordID
                },
                new BookKeyword{
                    BookID = books.Single(b => b.Title == "Solaris").BookID,
                    KeywordID = keywords.Single(k => k.KeywordID == "artificial intelligence").KeywordID
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
