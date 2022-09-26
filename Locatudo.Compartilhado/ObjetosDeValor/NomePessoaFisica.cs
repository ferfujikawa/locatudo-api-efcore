namespace Locatudo.Compartilhado.ObjetosDeValor
{
    public class NomePessoaFisica
    {
        public NomePessoaFisica(string primeiroNome, string sobrenome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
        }

        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }

        public override string ToString()
        {
            return $"{PrimeiroNome} {Sobrenome}";
        }
    }
}
