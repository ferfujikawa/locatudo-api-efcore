using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Domain.Executores.Comandos.Entradas;
using Locatudo.Domain.Tests.Customizacoes;
using Moq;
using Locatudo.Domain.Entidades;
using Locatudo.Domain.Repositorios;
using Locatudo.Domain.Executores;
using Locatudo.Domain.Executores.Comandos.Saidas;
using Locatudo.Shared.Enumerators;
using Locatudo.Shared.ValueObjects;

namespace Locatudo.Domain.Tests.Executores
{
    public class TestesExecutorReprovarLocacao
    {
        [Theory, AutoMoq]
        public void Comando_Valido_ReprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de Mocks
            var departamento = fixture.Create<Departamento>();
            var aprovador = fixture.Create<Funcionario>();
            var equipamento = fixture.Create<Equipamento>();

            //Alteração de propriedades dos mocks
            aprovador.AlterarLotacao(departamento);
            equipamento.AlterarGerenciador(departamento);

            //Criação do mock de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Funcionario>(), fixture.Create<RentalTime>())));
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, aprovador.Id);

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            resultado.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<DadoRespostaComandoReprovarLocacao>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.Should().Match<DadoRespostaComandoReprovarLocacao>(x => x.IdAprovador.Equals(comando.IdAprovador) && x.Situacao.Equals(ERentalStatus.Disapproved.ToString()), "Ao reprovar a locação, a situação deve ser alterada para Disapproved e Id do aprovador da locação precisa ser o mesmo passado no comando");
        }

        [Theory, AutoMoq]
        public void Comando_Invalido_GerarNotificacao(IFixture fixture)
        {
            ////Arrange
            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao();

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarNotificacao(IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Criação de mocks
            var aprovador = fixture.Create<Funcionario>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(Guid.NewGuid(), aprovador.Id);

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Locação não encontrada.", "Quando informado o Id de uma locação inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Aprovador_Invalido_GerarExcecao(IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Funcionario?)null);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, Guid.NewGuid());

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Funcionário não encontrado.", "Quando informado o Id de um funcionário inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Aprovador_NaoLotadoEmDepartamentoGerenciador_GerarNotificacao(
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
            aprovador.AlterarLotacao(fixture.Create<Departamento>());
            equipamento.AlterarGerenciador(departamento);

            //Criação do mock de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Terceirizado>(), fixture.Create<RentalTime>())));
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, Guid.NewGuid());

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Aprovador não está lotado no departamento gerenciador do equipamento.", "Quando o aprovador não estiver lotado departamento gerenciador do equipamento, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void SituacaoAtual_NaoPermiteReprovacao_GerarNotificacao(
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
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Terceirizado>(), fixture.Create<RentalTime>())));
            var locacao = fixture.Create<Locacao>();
            locacao.Aprovar(aprovador);

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, Guid.NewGuid());

            //Act
            var resultado = executor.Handle(comando);

            //Assert
            resultado.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            resultado.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("A situação atual da locação não permite reprovação.", "Locações aprovadas não podem ser reprovadas");
        }
    }
}
