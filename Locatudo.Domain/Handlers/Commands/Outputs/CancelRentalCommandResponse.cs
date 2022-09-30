using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers.Commands.Outputs
{
    public class CancelRentalCommandResponse : ICommandData
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        public CancelRentalCommandResponse(Guid id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
