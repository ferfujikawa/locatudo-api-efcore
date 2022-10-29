using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Responses
{
    public class CancelRentalData : ICommandData
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        public CancelRentalData(Guid id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
