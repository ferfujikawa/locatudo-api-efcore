using Locatudo.Shared.Entities;

namespace Locatudo.Domain.Entities
{
    public class Equipment : BaseEntity
    {
        public Equipment(string name) : base()
        {
            Name = name;
        }

        public string Name { get; private set; }
        public Department? Manager { get; private set; }

        public void ChangeManager(Department manager)
        {
            Manager = manager;
        }
    }
}
