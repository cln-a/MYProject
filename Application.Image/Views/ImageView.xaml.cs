using System.Windows.Controls;

namespace Application.Image
{
    /// <summary>
    /// IamgeView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageView : UserControl
    {
        public ImageView()
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
