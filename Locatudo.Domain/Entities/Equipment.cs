using Locatudo.Shared.Entities;

namespace Locatudo.Domain.Entities
{
    public class Equipment : BaseEntity
    {
        public string Name { get; private set; }

        public Guid? ManagerId { get; private set; }

        private Department? _manager;

        public Department? Manager {
            get => _manager;
            private set {
                _manager = value;
                ManagerId = value?.Id;
            }
        }

        protected Equipment() : base() { }

        public Equipment(string name) : base()
        {
            Name = name;
        }

        public void ChangeManager(Department manager)
        {
            Manager = manager;
        }
    }
}
