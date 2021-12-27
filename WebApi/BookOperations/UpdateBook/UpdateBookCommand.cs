using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{

    public class UpdateBookCommand
    {
        public UpdateBookModel Model{ get; set;}
        private readonly BookStoreDbContext _dbContext;
        public int BookId {get; set;}

        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id == BookId);

            if(book is null) throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;           
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.SaveChanges();
      
        }

        public class UpdateBookModel
        {
            public string Title {get;set;}
            public int GenreId { get; set; }
            
        }
    }
}