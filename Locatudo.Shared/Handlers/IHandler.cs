using Locatudo.Shared.Handlers.Commands.Input;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Shared.Handlers
{
    public interface IHandler<T, U> where T : IHandlerCommand where U : IHandlerCommandData
    {
        IHandlerCommandResponse<U> Handle(T command);
    }
}
