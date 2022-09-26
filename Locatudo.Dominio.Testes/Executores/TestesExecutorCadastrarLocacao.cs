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
    public class TestesExecutorCadastrarLocacao
    {
        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor e comando
            var executor = fixture.Create<ExecutorCadastrarLocacao>();
            var comando = new ComandoCadastrarLocacao(equipamento.Id, terceirizado.Id, DateTime.Now.AddHours(1));

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");

            resultado.Dado
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Dado")
                .And.BeOfType<DadoRespostaComandoCadastrarLocacao>("Resultados com sucesso devem ter a propriedade Dado de um tipo específico")
                .Which.Situacao.Should().Be(ESituacaoLocacao.Solicitado.ToString(), "A situação de uma nova locação deve ser Solicitado");
        }

        [Theory, AutoMoq]
        public void Comando_Invalido_GerarNotificacao(IFixture fixture)
        {
            ////Arrange
            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCadastrarLocacao>();
            var comando = new ComandoCadastrarLocacao();

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Comando_EquipamentoInvalido_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Equipamento?) null);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor e comando
            var executor = fixture.Create<ExecutorCadastrarLocacao>();
            var comando = new ComandoCadastrarLocacao(Guid.NewGuid(), terceirizado.Id, DateTime.Now.AddHours(1));

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Equipamento não encontrado", "Quando informado o Id de um equipamento inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Comando_LocadorInvalido_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Terceirizado?) null);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor e comando
            var executor = fixture.Create<ExecutorCadastrarLocacao>();
            var comando = new ComandoCadastrarLocacao(equipamento.Id, Guid.NewGuid(), DateTime.Now.AddHours(1));

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Usuário não encontrado", "Quando informado o Id de um usuário inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Comando_DataIndisponivel_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(false);

            //Criação do mock do executor e comando
            var executor = fixture.Create<ExecutorCadastrarLocacao>();
            var comando = new ComandoCadastrarLocacao(equipamento.Id, Guid.NewGuid(), DateTime.Now.AddHours(1));

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Horário de locação indisponível", "Quando informado um horário indisponível, o resultado deve conter uma notificação específica");
        }
    }
}
