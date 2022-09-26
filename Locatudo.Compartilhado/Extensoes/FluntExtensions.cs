using Flunt.Validations;

namespace Locatudo.Compartilhado.Extensoes
{
    public static class FluntExtensions
    {
        public static Contract<T> HasMinLength<T>(this Contract<T> contract, string val, int comparer, string key, string message)
        {
            if (string.IsNullOrEmpty(val) || val.Length < comparer)
                contract.AddNotification(key, message);
                
            return contract;
        }

        public static Contract<T> HasMaxLength<T>(this Contract<T> contract, string val, int comparer, string key, string message)
        {
            if (string.IsNullOrEmpty(val))
                return contract;

            if (val.Length > comparer)
                contract.AddNotification(key, message);

            return contract;
        }
    }
}
