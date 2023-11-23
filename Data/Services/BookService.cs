using libreriaa_JADM.Data.ViewModels;
using libreriaa_JADM.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace libreriaa_JADM.Data.Services
{
    public class BookService
    {
        private AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Titulo = book.Titulo,
                Descripcion = book.Descripcion,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genero = book.Genero,
                CoverUrl = book.CoverUrl,
                DateAdded = System.DateTime.Now,
                PublisherId = book.PublisherID
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AutorIDs)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.id,
                    AuthorId = id
                };
                _context.Book_Author.Add(_book_author);
                _context.SaveChanges() ;
            }
                
        }
        //Metodo q permite obtener una lista de todos los libros
        public List<Book> GetAllBks() => _context.Books.ToList();
        //metodos q nos permite obtener el libro que pedimos de la bd
        public BookWithAuthorsVM GetBookById(int bookid)
        {
            var _bookWithAuthors = _context.Books.Where(n=> n.id == bookid).Select(book => new BookWithAuthorsVM()
            {
                Titulo = book.Titulo,
                Descripcion = book.Descripcion,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genero = book.Genero,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AutorName = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return _bookWithAuthors;
        }
    //metodo paa modificar un libro de la bd
    public  Book UpdateBookById(int bookid, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.id == bookid);
            if (_book != null)
            {
                _book.Titulo = book.Titulo;
                  _book.Descripcion = book.Descripcion;
                  _book.IsRead = book.IsRead;
                  _book.DateRead = book.DateRead;
                  _book.Rate = book.Rate;
                  _book.Genero = book.Genero;
                  _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
                    }
            return _book;
       }
        public void DeleteBookById(int bookid)
        {
            var _book = _context.Books.FirstOrDefault(n => n.id == bookid);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
