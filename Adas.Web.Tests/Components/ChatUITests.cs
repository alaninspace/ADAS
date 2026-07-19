using Bunit;
using Xunit;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Adas.Web.Pages;
using Adas.Web.Services;
using System.Threading.Tasks;

namespace Adas.Web.Tests.Components
{
    public class ChatUITests : BunitContext, IAsyncLifetime
    {
        public ChatUITests()
        {
            JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddMudServices();
            Services.AddSingleton<ChatStateService>();
        }

        [Fact]
        public void ChatUI_ShouldContainModelPicker()
        {
            // Act
            var cut = Render<Home>();

            // Assert
            Assert.Contains("gpt-5.6-terra", cut.Markup);
        }

        [Fact]
        public void ChatUI_ShouldContainAgentSelector()
        {
            // Act
            var cut = Render<Home>();

            // Assert
            Assert.Contains("General Agent", cut.Markup);
        }

        [Fact]
        public void ChatUI_ShouldContainInputBoxAndSendButton()
        {
            // Act
            var cut = Render<Home>();

            // Assert
            Assert.Contains("Type a message...", cut.Markup);
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public new async Task DisposeAsync() => await base.DisposeAsync();
    }
}
