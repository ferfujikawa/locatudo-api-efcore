using Locatudo.Domain.Queries.Commands.Inputs;
using Locatudo.Domain.Queries.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Queries.Handlers
{
    public class DeleteOutsourcedHandler : IHandler<DeleteOutsourcedCommand, DeleteOutsourcedCommandResponse>
    {
        private readonly IOutsourcedRepository _outsourcedRepository;

        public DeleteOutsourcedHandler(IOutsourcedRepository outsourcedRepository)
        {
            _outsourcedRepository = outsourcedRepository;
        }

        public ICommandResponse<DeleteOutsourcedCommandResponse> Handle(DeleteOutsourcedCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<DeleteOutsourcedCommandResponse>(false, null, command.Notifications);

            var outsourced = _outsourcedRepository.GetById(command.Id);
            if (outsourced == null)
                return new GenericHandlerResponse<DeleteOutsourcedCommandResponse>(false, null, "Id", "Terceirizado não encontrado");

            _outsourcedRepository.Delete(outsourced);

            return new GenericHandlerResponse<DeleteOutsourcedCommandResponse>(
                true,
                new DeleteOutsourcedCommandResponse(),
                "Sucesso",
                "Terceirizado excluído");
        }
    }
}
