using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Testes.Customizacoes;
using FluentAssertions;
using AutoFixture;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCadastrarEquipamento
    {
        private readonly ComandoCadastrarEquipamento _comandoValido = new ("Equipamento teste 123");

        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarEquipamento(IFixture fixture)
        {
            //Arrange
            var executor = fixture.Create<ExecutorCadastrarEquipamento>();

            //Act
            var resultado = executor.Executar(_comandoValido);

            //Assert
            resultado.Successo.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            resultado.Dado
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Dado")
                .And.BeOfType<DadoRespostaComandoCadastrarEquipamento>("Resultados com sucesso devem ter a propriedade Dado de um tipo específico")
                .Which.Should().Match<DadoRespostaComandoCadastrarEquipamento>(x => x.Nome.Equals(_comandoValido.Nome), "O nome do novo equipamento deve ser igual ao que foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Comando_Invalido_GerarNotificacao(IFixture fixture)
        {
            ////Arrange
            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCadastrarEquipamento>();
            var comando = new ComandoCadastrarEquipamento();

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }
    }
}
