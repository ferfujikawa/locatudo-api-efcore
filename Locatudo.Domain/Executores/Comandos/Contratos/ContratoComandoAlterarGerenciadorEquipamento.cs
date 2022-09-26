using Flunt.Validations;
using Locatudo.Domain.Executores.Comandos.Entradas;

namespace Locatudo.Domain.Executores.Comandos.Contratos
{
    public class ContratoComandoAlterarGerenciadorEquipamento : Contract<ComandoAlterarGerenciadorEquipamento>
    {
        public ContratoComandoAlterarGerenciadorEquipamento(ComandoAlterarGerenciadorEquipamento comando)
        {
            Requires()
                .AreNotEquals(comando.Id, default, "IdEquipamento", "É necessário informar o IdEquipamento do equipamento para alterar seu gerenciador")
                .AreNotEquals(comando.IdDepartamento, default, "IdDepartamento", "É necessário informar o IdEquipamento do novo gerenciador do equipamento");
        }
    }
}
