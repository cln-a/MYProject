using Application.Common;
using Application.Login.Views;
using ApplicationFrameWork.ViewModels;
using MahApps.Metro.Controls;

namespace ApplicationFrameWork.Views;

public partial class ShellView : MetroWindow
{
    public ShellView()
    {
        InitializeComponent();
    }

    private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        var vm = DataContext as ShellViewModel;

        //加载模块
        vm?.ModuleManager.LoadModule(ConstName.ApplicationLoginModule);
        //导航区域
        vm?.RegionManager.RequestNavigate(ConstName.MainRegion, nameof(LoginView));
    }
}