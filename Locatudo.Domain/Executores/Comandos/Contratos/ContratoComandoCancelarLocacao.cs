using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Contratos
{
    public class ContratoComandoCancelarLocacao : Contract<ComandoCancelarLocacao>
    {
        public ContratoComandoCancelarLocacao(ComandoCancelarLocacao comando)
        {
            Requires()
                .AreNotEquals(comando.IdLocacao, default, "IdLocacao", "É necessário informar o IdEquipamento da locação que se pretende cancelar");
        }
    }
}
