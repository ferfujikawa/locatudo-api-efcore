using Locatudo.Compartilhado.Executores.Comandos.Entradas;
using Locatudo.Compartilhado.Executores.Comandos.Saidas;

namespace Locatudo.Compartilhado.Executores
{
    public interface IExecutor<T, U> where T : IComandoExecutor where U : IDadoRespostaComandoExecutor
    {
        IRespostaComandoExecutor<U> Executar(T comando);
    }
}
