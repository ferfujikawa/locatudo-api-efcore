using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Handlers;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Tests.Customizations;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Domain.Tests.Handlers
{
    public class ChangeEquipmentManagerHandlerTests
    {
        [Theory, AutoMoq]
        public void Command_Valid_ChangeEquipmentManagerHandler(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IDepartmentRepository> departmentRepository)
        {
            //Arrange
            //Criação de mocks
            var equipment = fixture.Create<Equipment>();
            var department = fixture.Create<Department>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(equipment);
            departmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(department);

            //Mock de handler e instância de command
            var handler = fixture.Create<ChangeEquipmentManagerHandler>();
            var command = new ChangeEquipmentManagerCommand(equipment.Id, department.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<ChangeEquipmentManagerCommandResponse>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.DepartmentId.Should().Be(department.Id, "O departamento gerenciador do equipamento precisa ser o mesmo cujo EquipmentId foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Command_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<ChangeEquipmentManagerHandler>();
            var command = new ChangeEquipmentManagerCommand();

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Equipment_Invalid_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IDepartmentRepository> departmentRepository)
        {
            //Arrange
            //Criação de mocks
            var department = fixture.Create<Department>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Equipment?)null);
            departmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(department);

            //Mock de handler e instância de command
            var handler = fixture.Create<ChangeEquipmentManagerHandler>();
            var command = new ChangeEquipmentManagerCommand(Guid.NewGuid(), department.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Equipamento não encontrado", "Quando informado o EquipmentId de um equipamento inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Department_Invalid_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IDepartmentRepository> departmentRepository)
        {
            //Arrange
            //Criação de mocks
            var equipment = fixture.Create<Equipment>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(equipment);
            departmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Department?)null);

            //Mock de handler e instância de command
            var handler = fixture.Create<ChangeEquipmentManagerHandler>();
            var command = new ChangeEquipmentManagerCommand(equipment.Id, Guid.NewGuid());

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Departamento não encontrado", "Quando informado o EquipmentId de um departamento inexistente, o resultado deve conter uma notificação específica");
        }
    }
}
