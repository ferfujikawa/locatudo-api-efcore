using Locatudo.Shared.Handlers.Commands.Input;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Shared.Handlers
{
    public interface IHandler<T, U> where T : ICommand where U : ICommandData
    {
        ICommandResponse<U> Handle(T command);
    }
}
