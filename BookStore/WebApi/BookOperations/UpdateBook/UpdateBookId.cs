using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookId
    {
        public UpdateBookIdViewModel Model;
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookId(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;   
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id==BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap mevcut değil");

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

            _dbContext.SaveChanges();
            
        }
    }

    public class UpdateBookIdViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }

}