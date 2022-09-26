using Locatudo.Shared.Executores.Comandos.Entradas;
using Locatudo.Shared.Executores.Comandos.Saidas;

namespace Locatudo.Shared.Executores
{
    public interface IExecutor<T, U> where T : IComandoExecutor where U : IDadoRespostaComandoExecutor
    {
        IRespostaComandoExecutor<U> Executar(T comando);
    }
}
