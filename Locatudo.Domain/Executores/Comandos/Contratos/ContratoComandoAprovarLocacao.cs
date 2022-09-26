using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Contratos
{
    public class ContratoComandoAprovarLocacao : Contract<ComandoAprovarLocacao>
    {
        public ContratoComandoAprovarLocacao(ComandoAprovarLocacao comando)
        {
            Requires()
                .AreNotEquals(comando.IdLocacao, default, "IdLocacao", "É necessário informar o IdEquipamento da locação que se pretende aprovar")
                .AreNotEquals(comando.IdAprovador, default, "IdAprovador", "É necessário informar o IdEquipamento do funcionário que está aprovando a locacação");
        }
    }
}
