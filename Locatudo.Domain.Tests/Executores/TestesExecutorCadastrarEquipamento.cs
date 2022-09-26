using Locatudo.Domain.Tests.Customizacoes;
using FluentAssertions;
using AutoFixture;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Executores;

namespace Locatudo.Domain.Tests.Executores
{
    public class TestesExecutorCadastrarEquipamento
    {
        private readonly ComandoCadastrarEquipamento _comandoValido = new("Equipamento teste 123");

        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarEquipamento(IFixture fixture)
        {
            //Arrange
            var executor = fixture.Create<ExecutorCadastrarEquipamento>();

            //Act
            var resultado = executor.Handle(_comandoValido);

            //Assert
            resultado.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            resultado.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<DadoRespostaComandoCadastrarEquipamento>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
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
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }
    }
}
