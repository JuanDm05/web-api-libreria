using libreriaa_JADM.Data.Models;
using libreriaa_JADM.Data.ViewModels;
using System;
using System.Linq;

namespace libreriaa_JADM.Data.Services

{
    public class AuthorService
    {
        private AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();

        }
        public AuthorWithBooksVM GetAuthorWithBooks(int authorId) 
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(N => N.Book.Titulo).ToList()
            }).FirstOrDefault();    
            return _author;
        }
    }
}
