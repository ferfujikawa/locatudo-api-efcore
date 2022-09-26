namespace Locatudo.Compartilhado.Executores.Comandos.Saidas
{
    public interface IRespostaComandoExecutor<T> where T : IDadoRespostaComandoExecutor
    {
        bool Successo { get; }
        T? Dado { get; }
        IReadOnlyCollection<string> Mensagens { get; }
    }
}
