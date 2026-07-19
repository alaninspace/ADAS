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
            Assert.Contains("Model Picker", cut.Markup);
        }

        [Fact]
        public void ChatUI_ShouldContainAgentSelector()
        {
            // Act
            var cut = Render<Home>();

            // Assert
            Assert.Contains("Agent Selector", cut.Markup);
        }

        [Fact]
        public void ChatUI_ShouldContainInputBoxAndSendButton()
        {
            // Act
            var cut = Render<Home>();

            // Assert
            Assert.Contains("Type message...", cut.Markup);
            Assert.Contains("Send", cut.Markup);
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public async Task DisposeAsync() => await base.DisposeAsync();
    }
}
