using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public List<BookViewModel> GetBooks()
        {
            GetBookQuery query = new GetBookQuery(_context,_mapper);
            var result = query.Handle();
            return result;
        }
        [HttpGet ("{id}")]
        public GetBookIdViewModel GetById(int id)
        {
            GetBookId command = new GetBookId(_context,_mapper);
            command.BookId=id;
            GetBookIdValidator validator =new GetBookIdValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            return result;

        }
        
        // 2 ADET HTTPGET kullanıldığı için hata verir
        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book =BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //Post
        [HttpPost]
        public IActionResult Addbook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command); // UI da hatyı görebilmek için hatayı throw ettik
                command.Handle();

                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //         System.Console.WriteLine("Özellik "+ item.PropertyName + " - Error Message: " + item.ErrorMessage);
                        
                // else
                //     command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //Put
        [HttpPut ("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookIdViewModel updatedBook)
        {
            UpdateBookId update = new UpdateBookId(_context);
            try
            {
                update.Model=updatedBook;
                update.BookId=id;
                UpdateBookIdValidator validator = new UpdateBookIdValidator();
                validator.ValidateAndThrow(update);
                update.Handle();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                DeleteBookCommandValidator validator =new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}