using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class DeleteOutsourcedHandler : ICommandHandler<DeleteOutsourcedRequest, DeleteOutsourcedData>
    {
        private readonly IOutsourcedRepository _outsourcedRepository;
        private readonly DeleteOutsourcedValidator _validator;

        public DeleteOutsourcedHandler(
            IOutsourcedRepository outsourcedRepository,
            DeleteOutsourcedValidator validator)
        {
            _outsourcedRepository = outsourcedRepository;
            _validator = validator;
        }

        public ICommandResponse<DeleteOutsourcedData> Handle(DeleteOutsourcedRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<DeleteOutsourcedData>(validationResult.Errors);

            var outsourced = _outsourcedRepository.GetById(request.Id);
            if (outsourced == null)
                return new GenericCommandHandlerResponse<DeleteOutsourcedData>("Terceirizado não encontrado");

            _outsourcedRepository.Delete(outsourced);

            return new GenericCommandHandlerResponse<DeleteOutsourcedData>(
                new DeleteOutsourcedData(),
                "Terceirizado excluído");
        }
    }
}
