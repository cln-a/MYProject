using System.Windows;

namespace Application.Startup
{
    public class SplashScreenManager(Window? loadWindow = null)
    {
        private readonly Window _splashScreenWindow = loadWindow ?? new MainSplashScreenView();

        public Window SplashScreenWindow => _splashScreenWindow;

        public bool TopMost { get => _splashScreenWindow.Topmost; set => _splashScreenWindow.Topmost = value; }

        public async void ShowAsync(Action initAction, Action loadAction)
        {
            await Task.Run(() =>
            {
                _splashScreenWindow.Dispatcher.Invoke(() =>
                {
                    _splashScreenWindow.Show();
                    _splashScreenWindow.Topmost = true;
                });

                initAction.Invoke();

                _splashScreenWindow.Dispatcher.Invoke(() =>
                {
                    loadAction.Invoke();
                    _splashScreenWindow.Close();
                });
            });
        }
    }
}
