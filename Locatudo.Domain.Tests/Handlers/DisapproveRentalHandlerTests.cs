using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Tests.Customizations;
using Moq;
using Locatudo.Shared.Enumerators;
using Locatudo.Shared.ValueObjects;
using Locatudo.Domain.Entities;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Handlers;

namespace Locatudo.Domain.Tests.Handlers
{
    public class DisapproveRentalHandlerTests
    {
        [Theory, AutoMoq]
        public void Command_Valid_DisapproveRental(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository,
            [Frozen] Mock<IEmployeeRepository> employeeRepository)
        {
            //Arrange
            //Resolução de dependência de classe abstrata User
            fixture.Inject<User>(fixture.Create<Employee>());

            //Criação de Mocks
            var department = fixture.Create<Department>();
            var appraiser = fixture.Create<Employee>();
            var equipment = fixture.Create<Equipment>();

            //Alteração de propriedades dos mocks
            appraiser.ChangeDepartament(department);
            equipment.ChangeManager(department);

            //Criação do mock de Rental
            fixture.Customize<Rental>(x => x.FromFactory(() => new Rental(equipment, fixture.Create<Employee>(), fixture.Create<RentalTime>())));
            var rental = fixture.Create<Rental>();

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(appraiser);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand(rental.Id, appraiser.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<DisapproveRentalCommandResponse>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.Should().Match<DisapproveRentalCommandResponse>(x => x.AppraiserId.Equals(command.AppraiserId) && x.Status.Equals(ERentalStatus.Disapproved.ToString()), "Ao reprovar a locação, a situação deve ser alterada para Disapproved e EquipmentId do aprovador da locação precisa ser o mesmo passado no comando");
        }

        [Theory, AutoMoq]
        public void Command_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand();

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Rental_Invalid_GenerateNotification(IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository,
            [Frozen] Mock<IEmployeeRepository> employeeRepository)
        {
            //Arrange
            //Criação de mocks
            var appraiser = fixture.Create<Employee>();

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(appraiser);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Rental?)null);

            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand(Guid.NewGuid(), appraiser.Id);

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Locação não encontrada.", "Quando informado o EquipmentId de uma locação inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Appraiser_Invalid_GenerateNotification(IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository,
            [Frozen] Mock<IEmployeeRepository> employeeRepository)
        {
            //Arrange
            //Resolução de dependência de classe abstrata User
            fixture.Inject<User>(fixture.Create<Employee>());

            //Criação de mocks
            var rental = fixture.Create<Rental>();

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Employee?)null);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand(rental.Id, Guid.NewGuid());

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Funcionário não encontrado.", "Quando informado o EquipmentId de um funcionário inexistente, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void Appraiser_NotInManagerDepartment_GenerateNotification(
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
            appraiser.ChangeDepartament(fixture.Create<Department>());
            equipment.ChangeManager(department);

            //Criação do mock de Rental
            fixture.Customize<Rental>(x => x.FromFactory(() => new Rental(equipment, fixture.Create<Outsourced>(), fixture.Create<RentalTime>())));
            var rental = fixture.Create<Rental>();

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(appraiser);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand(rental.Id, Guid.NewGuid());

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Appraiser não está lotado no departamento gerenciador do equipamento.", "Quando o aprovador não estiver lotado departamento gerenciador do equipamento, o resultado deve conter uma notificação específica");
        }

        [Theory, AutoMoq]
        public void CurrentStatus_DoNotAllowRepproval_Generateotification(
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
            rental.Approve(appraiser);

            //Setup de retornos de métodos dos repositórios
            employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(appraiser);
            rentalRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(rental);

            //Mock de handler e instância de command
            var handler = fixture.Create<DisapproveRentalHandler>();
            var command = new DisapproveRentalCommand(rental.Id, Guid.NewGuid());

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("A situação atual da locação não permite reprovação.", "Locações aprovadas não podem ser reprovadas");
        }
    }
}
