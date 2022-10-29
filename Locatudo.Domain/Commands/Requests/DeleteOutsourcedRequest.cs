using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class DeleteOutsourcedRequest : Notifiable<Notification>, ICommandRequest
    {
        public Guid Id { get; set; }

        public DeleteOutsourcedRequest()
        {
        }

        public DeleteOutsourcedRequest(Guid id)
        {
            Id = id;
        }

        public bool Validate()
        {
            AddNotifications(new DeleteOutsourcedContract(this));

            return IsValid;
        }
    }
}
