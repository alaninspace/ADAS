using System;

namespace Adas.Web.Services
{
    public class ThemeStateService
    {
        public bool IsDarkMode { get; private set; } = true;
        public event Action? OnChange;

        public void SetIsDarkMode(bool isDark)
        {
            if (IsDarkMode != isDark)
            {
                IsDarkMode = isDark;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
