using Flunt.Validations;
using Locatudo.Dominio.Executores.Comandos.Entradas;

namespace Locatudo.Dominio.Executores.Comandos.Contratos
{
    public class ContratoComandoAprovarLocacao : Contract<ComandoAprovarLocacao>
    {
        public ContratoComandoAprovarLocacao(ComandoAprovarLocacao comando)
        {
            Requires()
                .AreNotEquals(comando.IdLocacao, default(Guid), "IdLocacao", "É necessário informar o IdEquipamento da locação que se pretende aprovar")
                .AreNotEquals(comando.IdAprovador, default(Guid), "IdAprovador", "É necessário informar o IdEquipamento do funcionário que está aprovando a locacação");
        }
    }
}
