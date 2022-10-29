using AutoFixture;
using Moq;
using FluentAssertions;
using AutoFixture.Xunit2;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Tests.Customizations;
using Locatudo.Domain.Queries.Handlers;
using Locatudo.Domain.Queries.Requests;
using Locatudo.Domain.Queries.Responses;

namespace Locatudo.Domain.Tests.QueryHandlers
{
    public class GetRentalByIdHandlerTests
    {
        [Theory, AutoMoq]
        public void Request_Valid_ReturnRentalResponse(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Criação de mocks
            var rentalResponse = fixture.Create<RentalResponse>();

            //Setup de retornos de métodos dos repositórios
            rentalRepository.Setup(x => x.GetById<RentalResponse>(It.IsAny<Guid>())).Returns(rentalResponse);

            //Mock de handler e instância de command
            var handler = fixture.Create<GetRentalByIdHandler>();
            var request = new GetRentalByIdRequest(rentalResponse.Id);

            //Act
            var result = handler.Handle(request);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<RentalResponse>("Resultados com sucesso devem ter a propriedade Data de um tipo específico");
        }

        [Theory, AutoMoq]
        public void Request_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<GetRentalByIdHandler>();
            var request = new GetRentalByIdRequest();

            //Act
            var result = handler.Handle(request);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }

        [Theory, AutoMoq]
        public void Rental_NotFound_GenerateNotification(
            IFixture fixture,
            [Frozen] Mock<IRentalRepository> rentalRepository)
        {
            //Arrange
            //Setup de retornos de métodos dos repositórios
            rentalRepository.Setup(x => x.GetById<RentalResponse>(It.IsAny<Guid>())).Returns((RentalResponse?)null);

            //Mock de handler e instância de command
            var handler = fixture.Create<GetRentalByIdHandler>();
            var request = new GetRentalByIdRequest(Guid.NewGuid());

            //Act
            var result = handler.Handle(request);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação")
                .And.Contain("Locação não encontrada", "Quando informado o Id de uma locação inexistente, o resultado deve conter uma notificação específica");
        }
    }
}
