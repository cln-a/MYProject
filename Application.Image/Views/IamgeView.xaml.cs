using System.Windows.Controls;


namespace Application.Image.Views
{
    /// <summary>
    /// IamgeView.xaml 的交互逻辑
    /// </summary>
    public partial class IamgeView : UserControl
    {
        public IamgeView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (DataContext is ImageViewModel vm)
                    vm.HalconControl = HWinControl;
            };
        }
    }
}
