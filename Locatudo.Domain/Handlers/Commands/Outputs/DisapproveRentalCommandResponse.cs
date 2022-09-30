using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers.Commands.Outputs
{
    public class DisapproveRentalCommandResponse : ICommandData
    {
        public Guid Id { get; set; }
        public Guid AppraiserId { get; set; }
        public string AppraiserName { get; set; }
        public string Status { get; set; }

        public DisapproveRentalCommandResponse(Guid id, Guid appraiserId, string appraiserName, string status)
        {
            Id = id;
            AppraiserId = appraiserId;
            AppraiserName = appraiserName;
            Status = status;
        }
    }
}
