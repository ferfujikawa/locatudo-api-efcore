using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class DeleteOutsourcedRequest : ICommandRequest
    {
        public Guid Id { get; set; }

        public DeleteOutsourcedRequest()
        {
        }

        public DeleteOutsourcedRequest(Guid id)
        {
            Id = id;
        }
    }
}
