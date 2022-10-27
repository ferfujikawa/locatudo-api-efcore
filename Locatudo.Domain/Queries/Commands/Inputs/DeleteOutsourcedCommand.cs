using Flunt.Notifications;
using Locatudo.Domain.Queries.Commands.Contracts;
using Locatudo.Shared.Handlers.Commands.Input;

namespace Locatudo.Domain.Queries.Commands.Inputs
{
    public class DeleteOutsourcedCommand : Notifiable<Notification>, ICommand
    {
        public Guid Id { get; set; }

        public DeleteOutsourcedCommand()
        {
        }

        public DeleteOutsourcedCommand(Guid id)
        {
            Id = id;
        }

        public bool Validate()
        {
            AddNotifications(new DeleteOutsourcedCommandContract(this));

            return IsValid;
        }
    }
}
