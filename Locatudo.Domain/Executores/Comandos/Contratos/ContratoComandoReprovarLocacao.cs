using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Contratos
{
    public class ContratoComandoReprovarLocacao : Contract<ComandoReprovarLocacao>
    {
        public ContratoComandoReprovarLocacao(ComandoReprovarLocacao comando)
        {
            Requires()
                .AreNotEquals(comando.IdLocacao, default, "IdLocacao", "É necessário informar o IdEquipamento da locação que se pretende reprovar")
                .AreNotEquals(comando.IdAprovador, default, "IdAprovador", "É necessário informar o IdEquipamento do funcionário que está reprovando a locacação");
        }
    }
}
