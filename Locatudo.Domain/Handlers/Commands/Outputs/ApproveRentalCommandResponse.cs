using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers.Commands.Outputs
{
    public class ApproveRentalCommandResponse : ICommandData
    {
        public Guid RentalId { get; set; }
        public Guid AppraiserId { get; set; }
        public string AppraiserName { get; set; }
        public string Status { get; set; }

        public ApproveRentalCommandResponse(Guid rentalId, Guid appraiserId, string appraiserName, string status)
        {
            RentalId = rentalId;
            AppraiserId = appraiserId;
            AppraiserName = appraiserName;
            Status = status;
        }
    }
}
