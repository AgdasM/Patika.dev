using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookIdValidator : AbstractValidator<GetBookId>
    {
        public GetBookIdValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}