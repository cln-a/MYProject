# ApplicationFrameWork

## 项目概述

ApplicationFrameWork 是一个基于 WPF 和 Prism 框架的工业自动化应用程序框架，支持多语言国际化，提供完整的工位控制、通信监控、图像处理等功能。

## 项目架构

### 核心模块

- **Application.Main**: 主界面模块，负责菜单导航和语言切换
- **Application.Login**: 登录模块，支持多语言登录界面
- **Application.GeneralControl**: 通用控制模块，提供工位启用/禁用和时间参数设置
- **Application.Communicate**: 通信监控模块，支持 Modbus 和 S7net 协议
- **Application.Camera**: 相机模块，支持海康威视相机
- **Application.ImageProcess**: 图像处理模块，基于 Halcon 引擎
- **Application.ArtificialIntelligence**: AI 模块，集成大语言模型
- **Application.UI**: UI 基础模块，提供多语言支持和通用组件

### 技术栈

- **前端**: WPF + Prism + HandyControl + MahApps.Metro
- **后端**: .NET 8.0 + Unity IoC 容器
- **数据库**: SQLite (SqlSugar ORM)
- **通信**: Modbus TCP/RTU, S7net (西门子 PLC)
- **图像处理**: Halcon
- **AI**: 大语言模型 API 集成

## 多语言支持

### 支持的语言
- 简体中文 (CN)
- 英语 (US) 
- 俄语 (Russia)

### 语言切换机制
1. 通过 `LanguageManager` 类管理语言资源
2. 使用 XAML ResourceDictionary 存储多语言文本
3. 支持运行时动态切换语言
4. 所有 UI 文本和弹出消息都支持多语言

### 语言资源文件
- `Application.UI/Language/CN.xaml`: 中文语言资源
- `Application.UI/Language/US.xaml`: 英文语言资源  
- `Application.UI/Language/Russia.xaml`: 俄文语言资源

## 主要功能

### 1. 工位控制
- 支持 11 个工位的独立启用/禁用控制
- 可设置每个工位的耗时时间和延时时间
- 实时监控工位状态和处理时间

### 2. 通信监控
- Modbus TCP/RTU 协议支持
- 西门子 S7net 协议支持
- 实时数据读写和监控
- 通信状态监控

### 3. 图像处理
- 基于 Halcon 的图像处理引擎
- 支持图像采集、处理和分析
- 生产者-消费者模式处理图像数据

### 4. AI 集成
- 大语言模型 API 集成
- 支持多种模型选择
- 可配置的 AI 参数设置

## 使用方法

### 1. 语言切换
在登录界面或主界面底部选择对应的语言按钮即可切换界面语言。

### 2. 工位控制
1. 进入通用控制界面
2. 设置各工位的耗时时间和延时时间
3. 点击对应工位的启用按钮进行控制

### 3. 通信监控
1. 进入通信监控界面
2. 配置 Modbus 或 S7net 设备参数
3. 实时监控和读写数据

## 开发指南

### 添加新的多语言文本
1. 在对应的语言资源文件中添加新的键值对
2. 在代码中使用 `LanguageManager` 获取多语言文本
3. 确保所有语言文件都包含相同的键

### 添加新的弹出消息
1. 在语言资源文件中定义消息键
2. 使用 `PopupBox.Show(LanguageManager[key])` 显示多语言消息
3. 或者使用扩展方法 `LanguageManager.ShowLocalizedMessage(key)` 显示多语言消息
4. 对于带参数的消息，使用 `LanguageManager.ShowLocalizedMessage(key, parameter)` 或 `LanguageManager.ShowLocalizedMessage(key, parameters)`

### 模块开发
1. 继承 `IModule` 接口
2. 在 `RegisterTypes` 方法中注册依赖
3. 在 `OnInitialized` 方法中进行初始化

## 配置说明

### 数据库配置
配置文件: `Application.DAL/DbContextOption/DataBase.json`

### 通信配置
- Modbus: `Application.Modbus/modbussettings.json`
- S7net: 通过代码配置

## 注意事项

1. 所有硬编码的中文文本都应该使用多语言资源
2. 弹出消息必须支持多语言切换
3. 异常信息也应该支持多语言显示
4. 确保语言切换后所有界面元素都能正确更新

## 更新日志

### 最新版本
- 实现弹出对话框的多语言支持
- 优化语言切换机制
- 完善项目文档
- 添加 PopupBox 扩展方法，简化多语言消息显示
- 支持带参数的多语言消息格式化

### 多语言弹出消息实现
1. **语言资源文件更新**：
   - 在 `CN.xaml`、`US.xaml`、`Russia.xaml` 中添加了所有弹出消息的键值对
   - 包括工位控制、时间设置、系统响应等所有消息

2. **ViewModel 修改**：
   - `GeneralControlViewModel` 添加了 `ILanguageManager` 依赖注入
   - 所有 `PopupBox.Show()` 调用都改为使用多语言资源
   - `ModbusMonitorViewModel` 和 `ShellViewModel` 也进行了相应修改

3. **扩展方法**：
   - 创建了 `PopupBoxExtensions` 类，提供便捷的多语言消息显示方法
   - 支持简单消息、单参数消息和多参数消息的格式化

4. **使用方法**：
   ```csharp
   // 简单消息
   _languageManager.ShowLocalizedMessage("消息键");
   
   // 带参数的消息
   _languageManager.ShowLocalizedMessage("消息键", parameter);
   
   // 直接使用
   PopupBox.Show(_languageManager["消息键"]);
   ``` 