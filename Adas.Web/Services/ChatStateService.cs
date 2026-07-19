using System;

namespace Adas.Web.Services
{
    public class ChatStateService
    {
        public string SelectedModel { get; private set; } = "openai/gpt-5.6-terra";
        public string SelectedAgent { get; private set; } = "GeneralAgent";

        public event Action? OnChange;

        public void SetModel(string model)
        {
            if (SelectedModel != model)
            {
                SelectedModel = model;
                NotifyStateChanged();
            }
        }

        public void SetAgent(string agent)
        {
            if (SelectedAgent != agent)
            {
                SelectedAgent = agent;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
