using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookId
    {
        public GetBookIdViewModel Model {get;set;}
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext ;
        private readonly IMapper _mapper;

        public GetBookId(BookStoreDbContext dbcontext, IMapper mapper )
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public GetBookIdViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id ==BookId);
            if(book is  null)
                throw new InvalidOperationException("Kitap mevcut değil");

            book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            
            GetBookIdViewModel vm = _mapper.Map<GetBookIdViewModel>(book);
            //List<GetBookIdViewModel> vm = new List<GetBookIdViewModel>();
            // vm.Add(new GetBookIdViewModel(){
            //     Title=book.Title,
            //     Genre=((GenreEnum)book.GenreId).ToString(),
            //     PublishDate=book.PublishDate.Date.ToString("dd/mm/yyyy"),
            //     PageCount=book.PageCount
            //     });
            
            return vm;
        }
    }
    public class GetBookIdViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}