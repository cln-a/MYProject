using Application.Common;
using CommonServiceLocator;
using System.Windows.Controls;

namespace Application.RussiaUI
{
    /// <summary>
    /// RecordedDurationView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordedDurationView : UserControl
    {
        public RecordedDurationView()
        {
            InitializeComponent();

            if (DataContext is RecordedDurationViewModel vm)
            {
                vm.RefreshDataGridHeaderRequested += () =>
                {
                    var languageManager = ServiceLocator.Current.GetInstance<ILanguageManager>();

                    this.RecordedDurationDataGrid.Columns[0].Header = languageManager["工位序号"];
                    this.RecordedDurationDataGrid.Columns[1].Header = languageManager["当前工位耗时时间（开始）"];
                    this.RecordedDurationDataGrid.Columns[2].Header = languageManager["当前工位耗时时间（结束）"];
                };
            }
        }
    }
}
