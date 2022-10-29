using Locatudo.Shared.Commands.Requests;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Shared.Commands.Handlers
{
    public interface ICommandHandler<T, U> where T : ICommandRequest where U : ICommandData
    {
        ICommandResponse<U> Handle(T request);
    }
}
