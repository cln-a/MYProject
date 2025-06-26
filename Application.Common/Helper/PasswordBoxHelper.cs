using System.Windows.Controls;
using System.Windows;

namespace Application.Common
{
    public class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject obj) => (string)obj.GetValue(PasswordProperty);

        public static void SetPassword(DependencyObject obj, string value) => obj.SetValue(PasswordProperty, value);

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                // 解除事件绑定以避免递归
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                var newPassword = (string)e.NewValue;
                if (passwordBox.Password != newPassword)
                    passwordBox.Password = newPassword;

                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
                SetPassword(passwordBox, passwordBox.Password);
        }
    }
}
