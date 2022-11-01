using FluentValidation;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Validators
{
    public interface ICommandValidator<T> : IValidator<T> where T : ICommandRequest
    {
    }
}
