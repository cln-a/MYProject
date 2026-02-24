<<<<<<< HEAD
# ApplicationFrameWork 项目说明

## 项目概述
这是一个基于WPF的工业自动化应用程序框架，采用模块化架构设计，支持多语言（中文、英文、俄文）。

## 项目架构

### 核心模块
- **Application.Main**: 主应用程序模块，包含主界面和语言切换功能
- **Application.UI**: UI基础模块，包含语言管理器和基础样式
- **Application.Common**: 公共模块，包含枚举、事件、工具类等
- **Application.RussiaUI**: 俄罗斯UI模块，包含工位控制相关界面

### 功能模块
- **Application.Camera**: 相机控制模块
- **Application.Device**: 设备管理模块
- **Application.Communicate**: 通信模块
- **Application.Modbus**: Modbus协议模块
- **Application.S7net**: S7协议模块
- **Application.Dialog**: 对话框模块
- **Application.Login**: 登录模块

### 数据访问层
- **Application.DAL**: 数据访问层实现
- **Application.IDAL**: 数据访问层接口
- **Application.Model**: 数据模型

## 多语言支持
项目支持三种语言：
- 简体中文 (CN)
- 英文 (US)  
- 俄文 (Russia)

### 语言切换机制
1. 通过`LanguageManager`类管理语言资源
2. 语言资源存储在XAML文件中（CN.xaml, US.xaml, Russia.xaml）
3. 使用`DynamicResource`绑定实现界面文本的动态切换
4. 通过`LanguageChangedEvent`事件通知界面更新

## 使用说明

### 语言切换
在主界面通过菜单选择语言，系统会自动：
1. 更新当前语言类型
2. 重新加载语言资源
3. 发布语言变更事件
4. 更新菜单栏文本

### DataGrid列标题国际化
DataGrid的列标题通过以下方式实现国际化：

1. **绑定方式**：使用`{Binding Source={x:Static Application.Current}, Path=Resources[资源键]}`绑定
2. **事件订阅**：ViewModel订阅`LanguageChangedEvent`事件
3. **强制刷新**：语言切换时通过`OnPropertyChanged`和`RefreshHeaders`方法强制更新界面

#### 实现步骤：
1. 在XAML中使用绑定语法：`Header="{Binding Source={x:Static Application.Current}, Path=Resources[工位序号]}"`
2. 在ViewModel中订阅语言变更事件
3. 在语言变更处理中通知属性变更并刷新DataGrid

## 技术栈
- WPF (Windows Presentation Foundation)
- C# .NET Framework
- MVVM架构模式
- 模块化设计
- 事件驱动架构 
=======
hello my name is lonic penguin
>>>>>>> 9539b04e24fc36485a7590db1ea15478c636c986
