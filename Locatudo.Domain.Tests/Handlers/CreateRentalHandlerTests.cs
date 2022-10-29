using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Domain.Tests.Customizations;
using Moq;
using Locatudo.Shared.Enumerators;
using Locatudo.Shared.ValueObjects;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Handlers;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Tests.Handlers
{
    public class CreateRentalHandlerTests
    {
        [Theory, AutoMoq]
        public void Command_Valid_CreateRental(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Criação de mocks
            var equipment = fixture.Create<Equipment>();
            var outsourced = fixture.Create<Outsourced>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(equipment);
            userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(outsourced);
            rentalRepository.Setup(x => x.CheckAvailability(It.IsAny<Guid>(), It.IsAny<RentalTime>())).Returns(true);

            //Criação do mock do handler e command
            var handler = fixture.Create<CreateRentalHandler>();
            var command = new CreateRentalRequest(equipment.Id, outsourced.Id, DateTime.Now.AddHours(1));

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");

            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<CreateRentalData>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.Status.Should().Be(ERentalStatus.Requested.ToString(), "A situação de uma nova locação deve ser Requested");
        }

        [Theory, AutoMoq]
        public void Command_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<CreateRentalHandler>();
            var command = new CreateRentalRequest();

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Command_InvalidEquipment_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Criação de mocks
            var outsourced = fixture.Create<Outsourced>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Equipment?)null);
            userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(outsourced);
            rentalRepository.Setup(x => x.CheckAvailability(It.IsAny<Guid>(), It.IsAny<RentalTime>())).Returns(true);

            //Criação do mock do handler e command
            var handler = fixture.Create<CreateRentalHandler>();
            var command = new CreateRentalRequest(Guid.NewGuid(), outsourced.Id, DateTime.Now.AddHours(1));

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Equipamento não encontrado", "Quando informado o EquipmentId de um equipment inexistente, o result deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Command_InvalidRental_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Criação de mocks
            var equipment = fixture.Create<Equipment>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(equipment);
            userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Outsourced?)null);
            rentalRepository.Setup(x => x.CheckAvailability(It.IsAny<Guid>(), It.IsAny<RentalTime>())).Returns(true);

            //Criação do mock do handler e command
            var handler = fixture.Create<CreateRentalHandler>();
            var command = new CreateRentalRequest(equipment.Id, Guid.NewGuid(), DateTime.Now.AddHours(1));

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Usuário não encontrado", "Quando informado o EquipmentId de um usuário inexistente, o result deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Command_UnavailableTime_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IEquipmentRepository> equipmentRepository,
            [Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Criação de mocks
            var equipment = fixture.Create<Equipment>();
            var outsourced = fixture.Create<Outsourced>();

            //Setup de retornos de métodos dos repositórios
            equipmentRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(equipment);
            userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(outsourced);
            rentalRepository.Setup(x => x.CheckAvailability(It.IsAny<Guid>(), It.IsAny<RentalTime>())).Returns(false);

            //Criação do mock do handler e command
            var handler = fixture.Create<CreateRentalHandler>();
            var command = new CreateRentalRequest(equipment.Id, Guid.NewGuid(), DateTime.Now.AddHours(1));

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Horário de locação indisponível", "Quando informado um horário indisponível, o result deve conter uma notificação específica");
        }
    }
}
