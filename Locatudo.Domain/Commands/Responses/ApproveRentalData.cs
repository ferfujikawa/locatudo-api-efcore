using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class ApproveRentalData : ICommandData
    {
        public Guid RentalId { get; set; }
        public Guid AppraiserId { get; set; }
        public string AppraiserName { get; set; }
        public string Status { get; set; }

        public ApproveRentalData(Guid rentalId, Guid appraiserId, string appraiserName, string status)
        {
            RentalId = rentalId;
            AppraiserId = appraiserId;
            AppraiserName = appraiserName;
            Status = status;
        }
    }
}
