using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities;

public class Outsourced : User
{
    public Outsourced(PersonName name, Email email, EnterpriseName enterpriseName) : base(name, email)
    {
        EnterpriseName = enterpriseName;
    }

    public EnterpriseName EnterpriseName { get; private set; }
}
