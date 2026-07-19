using System;
using Xunit;
using Adas.Web.Services;

namespace Adas.Web.Tests
{
    public class ChatStateServiceTests
    {
        [Fact]
        public void DefaultState_ShouldHaveExpectedDefaults()
        {
            // Arrange
            var stateService = new ChatStateService();

            // Act & Assert
            Assert.Equal("gpt-5.6-terra", stateService.SelectedModel);
            Assert.Equal("GeneralAgent", stateService.SelectedAgent);
        }

        [Fact]
        public void UpdateModel_ShouldUpdatePropertyAndTriggerEvent()
        {
            // Arrange
            var stateService = new ChatStateService();
            bool eventTriggered = false;
            stateService.OnChange += () => eventTriggered = true;

            // Act
            stateService.SetModel("gpt-4o");

            // Assert
            Assert.Equal("gpt-4o", stateService.SelectedModel);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void UpdateAgent_ShouldUpdatePropertyAndTriggerEvent()
        {
            // Arrange
            var stateService = new ChatStateService();
            bool eventTriggered = false;
            stateService.OnChange += () => eventTriggered = true;

            // Act
            stateService.SetAgent("DbaAgent");

            // Assert
            Assert.Equal("DbaAgent", stateService.SelectedAgent);
            Assert.True(eventTriggered);
        }
    }
}
