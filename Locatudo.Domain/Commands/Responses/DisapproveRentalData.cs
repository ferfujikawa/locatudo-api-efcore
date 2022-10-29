using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class DisapproveRentalData : ICommandData
    {
        public Guid Id { get; set; }
        public Guid AppraiserId { get; set; }
        public string AppraiserName { get; set; }
        public string Status { get; set; }

        public DisapproveRentalData(Guid id, Guid appraiserId, string appraiserName, string status)
        {
            Id = id;
            AppraiserId = appraiserId;
            AppraiserName = appraiserName;
            Status = status;
        }
    }
}
