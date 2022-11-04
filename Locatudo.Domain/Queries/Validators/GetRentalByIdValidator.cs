using FluentValidation;
using Locatudo.Domain.Queries.Requests;

namespace Locatudo.Domain.Queries.Validators
{
    public class GetRentalByIdValidator : AbstractValidator<GetRentalByIdRequest>
    {
        public GetRentalByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("É necessário informar o Id da locação que se pretende obter");
        }
    }
}
