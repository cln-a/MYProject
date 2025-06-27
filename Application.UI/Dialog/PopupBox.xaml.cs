using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;


namespace Application.UI.Dialog
{
    /// <summary>
    /// PopupBox.xaml 的交互逻辑
    /// </summary>
    public partial class PopupBox : Popup
    {
        public PopupBox()
        {
            InitializeComponent();
        }
        
        public new double Opacity
        {
            get => (double)GetValue(OpacityProperty);
            set => SetValue(OpacityProperty, value);
        }

        public new static readonly DependencyProperty OpacityProperty = DependencyProperty.Register(
            nameof(Opacity), typeof(double), typeof(PopupBox),
            new PropertyMetadata(1.0, new PropertyChangedCallback(OnPropertyChangedCallback)));
        
        public string Message   
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(string), typeof(PopupBox), new PropertyMetadata(string.Empty));
        
        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PopupBox popupBox)) 
                return;
            if (!(e.NewValue is double newOpacity)) 
                return;
            popupBox.border.Opacity = newOpacity;
        }
        
        private static PopupBox dialog = new PopupBox();
        private static DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="owner">所属窗体对象</param>
        /// <param name="seconds">对话框隐藏倒计时（秒）</param>
        public static void Show(string message,Window owner = null!,int seconds = 1)
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (owner == null)
                        owner = System.Windows.Application.Current.MainWindow!;
                    dialog.Message = message;
                    dialog.PlacementTarget = owner;
                    dialog.Placement = PlacementMode.Center;
                    dialog.StaysOpen = true;
                    dialog.AllowsTransparency = true;
                    dialog.VerticalOffset = owner.ActualHeight / 3;
                    dialog.Opacity = 0.9;
                    dialog.IsOpen = true;

                    timer.Tick -= TimerOnTick;
                    timer.Tick += TimerOnTick;
                    timer.Interval = new TimeSpan(0,0,seconds);
                    timer.Start();  
                }));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static void TimerOnTick(object? sender, EventArgs e)
        {
            timer.Stop();
            Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(5);
                        if (System.Windows.Application.Current == null)
                            return;
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            dialog.Opacity -= 0.1;
                        });
                    }

                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        dialog.IsOpen = false;
                        dialog.Message = string.Empty;  
                    });
                }
                catch (Exception e)
                {
                }
            });
        }
    }
}
