using Bunit;
using Xunit;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Adas.Web.Layout;
using System.Threading.Tasks;

namespace Adas.Web.Tests.Components
{
    public class MainLayoutTests : BunitContext, IAsyncLifetime
    {
        public MainLayoutTests()
        {
            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void MainLayout_ShouldContainMudThemeProvider()
        {
            // Arrange
            Services.AddMudServices();
            Services.AddSingleton<Adas.Web.Services.ThemeStateService>();

            // Act
            var cut = Render<MainLayout>();

            // Assert
            Assert.NotNull(cut.FindComponent<MudThemeProvider>());
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public new async Task DisposeAsync() => await base.DisposeAsync();
    }
}
