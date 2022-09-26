using Locatudo.Shared.Enumerators;

namespace Locatudo.Shared.ValueObjects
{
    public class RentalStatus
    {
        public ERentalStatus Value { get; private set; }
        public RentalStatus()
        {
            Value = ERentalStatus.Requested;
        }

        public bool Approve()
        {
            if (Value == ERentalStatus.Requested)
            {
                Value = ERentalStatus.Approved;
                return true;
            }
            return false;
        }
        public bool Disapprove()
        {
            if (Value == ERentalStatus.Requested)
            {
                Value = ERentalStatus.Disapproved;
                return true;
            }
            return false;
        }

        public bool Cancel()
        {
            ERentalStatus[] situacoesPossiveis = { ERentalStatus.Requested, ERentalStatus.Approved };
            if (situacoesPossiveis.Contains(Value))
            {
                Value = ERentalStatus.Canceled;
                return true;
            }
            return false;
        }
    }
}
