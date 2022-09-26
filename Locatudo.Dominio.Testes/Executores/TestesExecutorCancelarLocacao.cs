using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Compartilhado.Enumeradores;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;
using Locatudo.Dominio.Testes.Customizacoes;
using Moq;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCancelarLocacao
    {
        [Theory, AutoMoq]
        public void Comando_Valido_AprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao(locacao.Id);

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            resultado.Dado
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Dado")
                .And.BeOfType<DadoRespostaComandoCancelarLocacao>("Resultados com sucesso devem ter a propriedade Dado de um tipo específico")
                .Which.Situacao.Should().Be(ESituacaoLocacao.Cancelado.ToString(), "Após aprovar a locação, a mesma deve ter situação Aprovado");
        }

        [Theory, AutoMoq]
        public void Comando_Invalido_GerarNotificacao(IFixture fixture)
        {
            ////Arrange
            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao();

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Setup de retornos de métodos dos repositórios
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao(Guid.NewGuid());

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Locação não encontrada.", "Quando informado o Id de uma locação inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void SituacaoAtual_NaoPermiteCancelamento_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var departamento = fixture.Create<Departamento>();
            var aprovador = fixture.Create<Funcionario>();
            var equipamento = fixture.Create<Equipamento>();

            //Alteração de propriedades de mocks
            aprovador.AlterarLotacao(departamento);
            equipamento.AlterarGerenciador(departamento);

            //Criação do mock de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Terceirizado>(), fixture.Create<HorarioLocacao>())));
            var locacao = fixture.Create<Locacao>();
            locacao.Reprovar(aprovador);

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao(locacao.Id);

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("A situação atual da locação não permite cancelamento.", "Locações reprovadas não podem ser canceladas");
        }
    }
}
