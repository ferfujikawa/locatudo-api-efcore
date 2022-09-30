using Locatudo.Domain.Tests.Customizations;
using FluentAssertions;
using AutoFixture;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Handlers;

namespace Locatudo.Domain.Tests.Handlers
{
    public class CreateEquipmentHandlerTests
    {
        [Theory, AutoMoq]
        public void Command_Valid_CreateEquipment(IFixture fixture)
        {
            //Arrange
            var handler = fixture.Create<CreateEquipmentHandler>();
            var command = new CreateEquipmentCommand("Equipament teste 123");

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeTrue("Resultados com sucesso devem ter o valor da propriedade Sucesso igual a verdadeiro");
            result.Data
                .Should().NotBeNull("Resultados com sucesso devem ter valor não nulo na propridade Data")
                .And.BeOfType<CreateEquipmentCommandResponse>("Resultados com sucesso devem ter a propriedade Data de um tipo específico")
                .Which.Should().Match<CreateEquipmentCommandResponse>(x => x.Name.Equals(command.Name), "O nome do novo equipamento deve ser igual ao que foi passado no command");
        }

        [Theory, AutoMoq]
        public void Command_Invalid_GenerateNotification(IFixture fixture)
        {
            ////Arrange
            //Mock de handler e instância de command
            var handler = fixture.Create<CreateEquipmentHandler>();
            var command = new CreateEquipmentCommand();

            //Act
            var result = handler.Handle(command);

            //Assert
            result.Success.Should().BeFalse("Resultados com falha devem ter o valor da propriedade Sucesso igual a falso");
            result.Data.Should().BeNull("Resultados com falha devem ter valor nulo na propridade Data");
            result.Messages.Should().NotBeEmpty("Resultados com falha devem ter alguma mensagem de notificação");
        }
    }
}
