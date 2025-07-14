using Application.Common;
using CommonServiceLocator;
using System.Windows.Controls;

namespace Application.GeneralControl
{
    /// <summary>
    /// SemiAutoView.xaml 的交互逻辑
    /// </summary>
    public partial class SemiAutoView : UserControl
    {
        public SemiAutoView()
        {
            InitializeComponent();

            if (DataContext is SemiAutoViewModel vm)
            {
                vm.RefreshDataGridHeaderRequested += () =>
                {
                    var languageManager = ServiceLocator.Current.GetInstance<ILanguageManager>();

                    this.TimeList.Columns[0].Header = languageManager["工位序号"];
                    this.TimeList.Columns[1].Header = languageManager["当前耗时"];
                    this.TimeList.Columns[2].Header = languageManager["当前工件处理时间（开始）"];
                    this.TimeList.Columns[3].Header = languageManager["当前工件处理时间（结束）"];
                };
            }
        }
    }
}
