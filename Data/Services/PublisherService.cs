using libreriaa_JADM.Data.Models;
using libreriaa_JADM.Data.ViewModels;
using libreriaa_JADM.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace libreriaa_JADM.Data.Services

{
    public class PublisherService
    {
        private AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }
        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStarsWithNumber(publisher.Name)) throw new PublisherNameException("El nombre  empieza  con un numero", publisher.Name);
            var _publisher = new Publisher()
            {
                Name =publisher.Name,
            };
            _context.Publisher.Add(_publisher);
            _context.SaveChanges();
            return _publisher;

        }

        public Publisher GetPublisherByID(int id) => _context.Publisher.FirstOrDefault(n => n.Id == id);     
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publisher.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Titulo,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()

                    }).ToList()
                }).FirstOrDefault();
        return _publisherData;
        }

        internal void DeletePublisherById(int id)
        {
            var _publisher = _context.Publisher.FirstOrDefault(n => n.Id == id);
            if(_publisher != null )
            {
                _context.Publisher.Remove(_publisher);  
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"La Editora con esa id : {id} no existe!");
            }
        }
        private bool StringStarsWithNumber(string name)
        {
            if(Regex.IsMatch(name, @"^\d")) return true;
            return false;
        }
    }
}
