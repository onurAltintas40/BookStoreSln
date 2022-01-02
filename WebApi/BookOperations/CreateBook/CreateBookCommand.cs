using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CretaeBook
{

    public class CreateBookCommand
    {
        private readonly IMapper _mapper;
        public CreateBookModel Model{ get; set;}
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext context,IMapper mapper)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);

            if(book is not null) throw new InvalidOperationException("Kitap zaten mevcut");
            book = _mapper.Map<Book>(Model);
            // book.Title = Model.Title;
            // book.GenreId = Model.GenreId;
            // book.PublishDate = Model.PublishDate;
            // book.PageCount = Model.PageCount;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();            
        }

        public class CreateBookModel
        {
            public string Title {get;set;}
            public int GenreId { get; set; }            
            public int PageCount { get; set; }
            public DateTime PublishDate{get ; set;}
        }
    }
}