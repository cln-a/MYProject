using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Application.UI
{
    /// <summary>
    /// TextBox输入行为类，用于限制TextBox的输入内容
    /// 支持整数和小数输入限制
    /// </summary>
    public static class TextBoxInputBehavior
    {
        #region 整数输入限制

        /// <summary>
        /// 整数输入限制的依赖属性
        /// </summary>
        public static readonly DependencyProperty IsNumericOnlyProperty =
            DependencyProperty.RegisterAttached(
                "IsNumericOnly",
                typeof(bool),
                typeof(TextBoxInputBehavior),
                new UIPropertyMetadata(false, OnIsNumericOnlyChanged));

        /// <summary>
        /// 获取整数输入限制状态
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <returns>是否只允许输入整数</returns>
        public static bool GetIsNumericOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericOnlyProperty);
        }

        /// <summary>
        /// 设置整数输入限制状态
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <param name="value">是否只允许输入整数</param>
        public static void SetIsNumericOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericOnlyProperty, value);
        }

        #endregion

        #region 小数输入限制

        /// <summary>
        /// 小数输入限制的依赖属性
        /// </summary>
        public static readonly DependencyProperty IsDecimalOnlyProperty =
            DependencyProperty.RegisterAttached(
                "IsDecimalOnly",
                typeof(bool),
                typeof(TextBoxInputBehavior),
                new UIPropertyMetadata(false, OnIsDecimalOnlyChanged));

        /// <summary>
        /// 小数位数限制的依赖属性
        /// </summary>
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.RegisterAttached(
                "DecimalPlaces",
                typeof(int),
                typeof(TextBoxInputBehavior),
                new UIPropertyMetadata(2));

        /// <summary>
        /// 获取小数输入限制状态
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <returns>是否只允许输入小数</returns>
        public static bool GetIsDecimalOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDecimalOnlyProperty);
        }

        /// <summary>
        /// 设置小数输入限制状态
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <param name="value">是否只允许输入小数</param>
        public static void SetIsDecimalOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDecimalOnlyProperty, value);
        }

        /// <summary>
        /// 获取小数位数限制
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <returns>小数位数</returns>
        public static int GetDecimalPlaces(DependencyObject obj)
        {
            return (int)obj.GetValue(DecimalPlacesProperty);
        }

        /// <summary>
        /// 设置小数位数限制
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <param name="value">小数位数</param>
        public static void SetDecimalPlaces(DependencyObject obj, int value)
        {
            obj.SetValue(DecimalPlacesProperty, value);
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 整数输入限制状态改变事件处理
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性改变事件参数</param>
        private static void OnIsNumericOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    // 添加事件处理器
                    textBox.PreviewTextInput += TextBox_PreviewTextInput_Integer;
                    DataObject.AddPastingHandler(textBox, OnPaste_Integer);
                    // 添加键盘事件处理，允许退格键和删除键
                    textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                }
                else
                {
                    // 移除事件处理器
                    textBox.PreviewTextInput -= TextBox_PreviewTextInput_Integer;
                    DataObject.RemovePastingHandler(textBox, OnPaste_Integer);
                    textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                }
            }
        }

        /// <summary>
        /// 小数输入限制状态改变事件处理
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性改变事件参数</param>
        private static void OnIsDecimalOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    // 添加事件处理器
                    textBox.PreviewTextInput += TextBox_PreviewTextInput_Decimal;
                    DataObject.AddPastingHandler(textBox, OnPaste_Decimal);
                    // 添加键盘事件处理，允许退格键和删除键
                    textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                }
                else
                {
                    // 移除事件处理器
                    textBox.PreviewTextInput -= TextBox_PreviewTextInput_Decimal;
                    DataObject.RemovePastingHandler(textBox, OnPaste_Decimal);
                    textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                }
            }
        }

        #endregion

        #region 文本输入验证

        /// <summary>
        /// 整数输入验证事件处理
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">文本组合事件参数</param>
        private static void TextBox_PreviewTextInput_Integer(object sender, TextCompositionEventArgs e)
        {
            // 检查输入是否为数字
            e.Handled = !IsTextInteger(e.Text);
        }

        /// <summary>
        /// 小数输入验证事件处理
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">文本组合事件参数</param>
        private static void TextBox_PreviewTextInput_Decimal(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // 获取小数位数限制
                int decimalPlaces = GetDecimalPlaces(textBox);
                // 检查输入是否为有效的小数
                e.Handled = !IsTextDecimal(e.Text, textBox.Text, decimalPlaces);
            }
        }

        /// <summary>
        /// 键盘事件处理，允许特殊按键
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">按键事件参数</param>
        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // 允许退格键、删除键、Tab键、方向键等
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || 
                e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down ||
                e.Key == Key.Home || e.Key == Key.End)
            {
                e.Handled = false;
            }
        }

        #endregion

        #region 粘贴验证

        /// <summary>
        /// 整数粘贴验证事件处理
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">数据对象粘贴事件参数</param>
        private static void OnPaste_Integer(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextInteger(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        /// <summary>
        /// 小数粘贴验证事件处理
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">数据对象粘贴事件参数</param>
        private static void OnPaste_Decimal(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (sender is TextBox textBox)
                {
                    int decimalPlaces = GetDecimalPlaces(textBox);
                    if (!IsTextDecimal(text, textBox.Text, decimalPlaces))
                    {
                        e.CancelCommand();
                    }
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 验证文本是否为有效整数
        /// </summary>
        /// <param name="text">要验证的文本</param>
        /// <returns>是否为有效整数</returns>
        private static bool IsTextInteger(string text)
        {
            // 使用正则表达式验证整数
            return Regex.IsMatch(text, @"^[0-9]+$");
        }

        /// <summary>
        /// 验证文本是否为有效小数
        /// </summary>
        /// <param name="inputText">输入的文本</param>
        /// <param name="currentText">当前TextBox的文本</param>
        /// <param name="decimalPlaces">允许的小数位数</param>
        /// <returns>是否为有效小数</returns>
        private static bool IsTextDecimal(string inputText, string currentText, int decimalPlaces)
        {
            // 如果输入的是小数点
            if (inputText == ".")
            {
                // 检查当前文本是否已经包含小数点
                if (currentText.Contains("."))
                {
                    return false;
                }
                return true;
            }

            // 如果输入的是数字
            if (Regex.IsMatch(inputText, @"^[0-9]$"))
            {
                // 检查添加输入后是否超过小数位数限制
                if (currentText.Contains("."))
                {
                    int decimalIndex = currentText.IndexOf('.');
                    int digitsAfterDecimal = currentText.Length - decimalIndex - 1;
                    if (digitsAfterDecimal >= decimalPlaces)
                    {
                        return false;
                    }
                }
                return true;
            }

            // 其他字符都不允许
            return false;
        }

        #endregion
    }
}
