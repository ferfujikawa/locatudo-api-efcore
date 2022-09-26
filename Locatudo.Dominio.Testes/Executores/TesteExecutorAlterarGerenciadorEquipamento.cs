using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos.Entradas;
using Locatudo.Dominio.Executores.Comandos.Saidas;
using Locatudo.Dominio.Repositorios;
using Locatudo.Dominio.Testes.Customizacoes;
using Moq;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TesteExecutorAlterarGerenciadorEquipamento
    {
        [Theory, AutoMoq]
        public void Comando_Valido_AlterarGerenciadorEquipamento(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var departamento = fixture.Create<Departamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(departamento);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(equipamento.Id, departamento.Id);

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            resultado.Dado
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Dado")
                .And.BeOfType<DadoRespostaComandoAlterarGerenciadorEquipamento>("Resultados com sucesso devem ter a propriedade Dado de um tipo específico")
                .Which.IdDepartamento.Should().Be(departamento.Id, "O departamento gerenciador do equipamento precisa ser o mesmo cujo IdEquipamento foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Comando_Invalido_GerarNotificacao(IFixture fixture)
        {
            ////Arrange
            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento();

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Equipamento_Invalido_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var departamento = fixture.Create<Departamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Equipamento?) null);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(departamento);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(Guid.NewGuid(), departamento.Id);

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Equipamento não encontrado", "Quando informado o Id de um equipamento inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Departamento_Invalido_GerarNotificacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Departamento?) null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(equipamento.Id, Guid.NewGuid());

            //Act
            var resultado = executor.Executar(comando);

            //Assert
            resultado.Successo.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            resultado.Dado.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Dado");
            resultado.Mensagens.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Departamento não encontrado", "Quando informado o Id de um departamento inexistente, o resultado deve conter uma notificação específica");
        }
    }
}
