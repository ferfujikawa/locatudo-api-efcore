using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class DeleteOutsourcedValidator : AbstractValidator<DeleteOutsourcedRequest>, ICommandValidator<DeleteOutsourcedRequest>
    {
        public DeleteOutsourcedValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("É necessário informar o Id do terceirizado que se pretende excluir");
        }
    }
}
