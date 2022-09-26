using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Locatudo.Domain.Tests.Customizacoes
{
    public class AutoMoqAttribute : AutoDataAttribute
    {
        public AutoMoqAttribute() : base(CriarFixture)
        {
        }

        private static IFixture CriarFixture()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            return fixture;
        }
    }
}
