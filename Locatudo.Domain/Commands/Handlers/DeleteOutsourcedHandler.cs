using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class DeleteOutsourcedHandler : ICommandHandler<DeleteOutsourcedRequest, DeleteOutsourcedData>
    {
        private readonly IOutsourcedRepository _outsourcedRepository;

        public DeleteOutsourcedHandler(IOutsourcedRepository outsourcedRepository)
        {
            _outsourcedRepository = outsourcedRepository;
        }

        public ICommandResponse<DeleteOutsourcedData> Handle(DeleteOutsourcedRequest request)
        {
            if (!request.Validate())
                return new GenericCommandHandlerResponse<DeleteOutsourcedData>(false, null, request.Notifications);

            var outsourced = _outsourcedRepository.GetById(request.Id);
            if (outsourced == null)
                return new GenericCommandHandlerResponse<DeleteOutsourcedData>("Id", "Terceirizado não encontrado");

            _outsourcedRepository.Delete(outsourced);

            return new GenericCommandHandlerResponse<DeleteOutsourcedData>(
                new DeleteOutsourcedData(),
                "Sucesso",
                "Terceirizado excluído");
        }
    }
}
