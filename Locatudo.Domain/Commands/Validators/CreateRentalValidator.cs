using FluentValidation;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public class CreateRentalValidator : AbstractValidator<CreateRentalRequest>, ICommandValidator<CreateRentalRequest>
    {
        public CreateRentalValidator()
        {
            RuleFor(x => x.EquipmentId).NotEmpty().WithMessage("É necessário informar o Id do equipamento que está sendo locado");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("É necessário informar o Id da usuário que está locando o equipamento");
            RuleFor(x => x.Start).NotEqual(new DateTime()).WithMessage("É necessário informar o horário de início da locação");
            RuleFor(x => x.Start).Must(NotBeInThePast).WithMessage("Início da locação não pode ser no passado");
        }
        private bool NotBeInThePast(DateTime date)
        {
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
            return date >= DateTime.Now;
        }
    }
}
