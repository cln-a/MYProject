using Application.Common;
using CommonServiceLocator;
using System.Windows.Controls;

namespace Application.RussiaUI
{
    /// <summary>
    /// GeneralControlView.xaml 的交互逻辑
    /// </summary>
    public partial class GeneralControlView : UserControl
    {
        public GeneralControlView()
        {
            InitializeComponent();

            if(DataContext is GeneralControlViewModel vm)
            {
                vm.RefreshDataGridHeaderRequested += () =>
                {
                    var languageManager = ServiceLocator.Current.GetInstance<ILanguageManager>();

                    this.WorkStationDataGrid.Columns[0].Header = languageManager["工位序号"];
                    this.WorkStationDataGrid.Columns[1].Header = languageManager["工位状态"];
                    this.WorkStationDataGrid.Columns[2].Header = languageManager["工位是否启用"];
                    this.WorkStationDataGrid.Columns[3].Header = languageManager["设定当前工位耗时时间"];
                    this.WorkStationDataGrid.Columns[4].Header = languageManager["设定当前工位延时时间"];
                    this.WorkStationDataGrid.Columns[5].Header = languageManager["设定"];


                    for (int i = 0; i <= 4; i++) 
                    {
                        this.WorkStationDataGrid.Columns[i].Width = new DataGridLength(0);
                        this.WorkStationDataGrid.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                    }
                };
            }
        }
    }
}
