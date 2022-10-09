using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Entities;

public class Outsourced : User
{
    public EnterpriseName EnterpriseName { get; private set; }


    protected Outsourced() { }
    public Outsourced(PersonName name, Email email, EnterpriseName enterpriseName) : base(name, email)
    {
        EnterpriseName = enterpriseName;
    }
}
