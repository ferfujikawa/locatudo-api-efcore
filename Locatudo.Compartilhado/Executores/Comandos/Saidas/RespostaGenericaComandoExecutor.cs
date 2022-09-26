using Flunt.Notifications;

namespace Locatudo.Compartilhado.Executores.Comandos.Saidas
{
    public class RespostaGenericaComandoExecutor<T> : IRespostaComandoExecutor<T> where T : IDadoRespostaComandoExecutor
    {
        public bool Successo { get; private set; }
        public T? Dado { get; private set; }
        public IReadOnlyCollection<string> Mensagens => _mensagens?.Select(x => x.Message).ToList() ?? new List<string>();

        private readonly IEnumerable<Notification>? _mensagens;

        public RespostaGenericaComandoExecutor(bool successo, T? dado, IEnumerable<Notification>? mensagens)
        {
            Successo = successo;
            Dado = dado;
            _mensagens = mensagens;
        }

        public RespostaGenericaComandoExecutor(bool successo, T? dado, string? chaveMensagem, string? mensagem)
        {
            Successo = successo;
            Dado = dado;
            _mensagens = new List<Notification>() { new Notification(chaveMensagem, mensagem) };
        }
    }
}
