using System.Windows.Controls;
using System.Windows.Data;
using Application.Common;

namespace Application.UI.Helper
{
    /// <summary>
    /// DataGrid列标题管理助手类
    /// 用于处理语言切换时DataGrid列标题的动态更新
    /// </summary>
    public static class DataGridHeaderHelper
    {
        /// <summary>
        /// 为DataGridTextColumn设置国际化标题
        /// </summary>
        /// <param name="column">DataGridTextColumn列</param>
        /// <param name="resourceKey">语言资源键</param>
        public static void SetLocalizedHeader(this DataGridTextColumn column, string resourceKey)
        {
            // 创建DynamicResource绑定
            var binding = new Binding
            {
                Source = System.Windows.Application.Current.Resources,
                Path = new System.Windows.PropertyPath($"[{resourceKey}]"),
                Mode = BindingMode.OneWay
            };
            
            // 设置列标题绑定
            column.Header = binding;
        }

        /// <summary>
        /// 为DataGridTemplateColumn设置国际化标题
        /// </summary>
        /// <param name="column">DataGridTemplateColumn列</param>
        /// <param name="resourceKey">语言资源键</param>
        public static void SetLocalizedHeader(this DataGridTemplateColumn column, string resourceKey)
        {
            // 创建DynamicResource绑定
            var binding = new Binding
            {
                Source = System.Windows.Application.Current.Resources,
                Path = new System.Windows.PropertyPath($"[{resourceKey}]"),
                Mode = BindingMode.OneWay
            };
            
            // 设置列标题绑定
            column.Header = binding;
        }

        /// <summary>
        /// 刷新DataGrid所有列的标题
        /// 当语言切换时调用此方法强制更新列标题
        /// </summary>
        /// <param name="dataGrid">要刷新的DataGrid</param>
        public static void RefreshHeaders(this DataGrid dataGrid)
        {
            // 通知DataGrid重新加载资源
            dataGrid.InvalidateVisual();
            
            // 强制刷新列标题
            foreach (var column in dataGrid.Columns)
            {
                if (column.Header is Binding binding)
                {
                    // 重新绑定以触发更新
                    var tempHeader = column.Header;
                    column.Header = null;
                    column.Header = tempHeader;
                }
            }
        }
    }
} 