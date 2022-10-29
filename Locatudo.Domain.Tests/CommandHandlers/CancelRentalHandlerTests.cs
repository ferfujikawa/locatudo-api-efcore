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

namespace Locatudo.Domain.Tests.CommandHandlers
{
    public class CancelRentalHandlerTests
    {
        [Theory, AutoMoq]
        public void Command_Valid_CancelRental(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Resolução de dependência de classe abstrata User
            fixture.Inject<User>(fixture.Create<Employee>());

            //Criação de mocks
            var rental = fixture.Create<Rental>();

            //Setup de retornos de métodos dos repositórios
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<CancelRentalHandler>();
            var command = new CancelRentalRequest(rental.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<CancelRentalData>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.Status.Should().Be(ERentalStatus.Canceled.ToString(), "Após aprovar a locação, a mesma deve ter situação Approved");
        }

        [Theory, AutoMoq]
        public void Command_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<CancelRentalHandler>();
            var command = new CancelRentalRequest();

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Rental_Invalid_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Setup de retornos de métodos dos repositórios
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Rental?)null);

            //Mock de handler e instância de command
            var handler = fixture.Create<CancelRentalHandler>();
            var command = new CancelRentalRequest(Guid.NewGuid());

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Locação não encontrada.", "Quando informado o EquipmentId de uma locação inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void CurrentStatus_DoNotAllowCancellation_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository,
            [Frozen] Mock<IEmployeeRepository> employeeRepository)
        {
            //Arrange
            //Resolução de dependência de classe abstrata User
            fixture.Inject<User>(fixture.Create<Employee>());

            //Criação de mocks
            var department = fixture.Create<Department>();
            var appraiser = fixture.Create<Employee>();
            var equipment = fixture.Create<Equipment>();

            //Alteração de propriedades de mocks
            appraiser.ChangeDepartament(department);
            equipment.ChangeManager(department);

            //Criação do mock de Rental
            fixture.Customize<Rental>(x => x.FromFactory(() => new Rental(equipment, fixture.Create<Outsourced>(), fixture.Create<RentalTime>())));
            var rental = fixture.Create<Rental>();
            rental.Disapprove(appraiser);

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(appraiser);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<CancelRentalHandler>();
            var command = new CancelRentalRequest(rental.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("A situação atual da locação não permite cancelamento.", "Locações reprovadas não podem ser canceladas");
        }
    }
}
