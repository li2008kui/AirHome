namespace ThisCoder.AirHome
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MessageType : byte
    {
        /// <summary>
        /// 服务器到设备
        /// </summary>
        ServerToDevice = 0X00,

        /// <summary>
        /// 设备反馈到服务器
        /// </summary>
        DeviceToServer = 0X01,

        /// <summary>
        /// 设备主动上报
        /// </summary>
        DeviceReport = 0X02,

        /// <summary>
        /// 服务器响应设备上报
        /// </summary>
        ServerResponse = 0X03
    }

    /// <summary>
    /// 消息ID枚举
    /// </summary>
    public enum MessageId : ushort
    {
        #region 配置功能
        /// <summary>
        /// 包含多个功能
        /// </summary>
        Multifunction = 0X0000,

        /// <summary>
        /// 设置模块或通道分区代码
        /// </summary>
        SettingModuleOrChannelPartitionCode = 0X0001,

        /// <summary>
        /// 设置模块或通道分区名称
        /// </summary>
        SettingModuleOrChannelPartitionName = 0X0002,

        /// <summary>
        /// 设置模块或通道名称
        /// </summary>
        SettingModuleOrChannelName = 0X0003,

        /// <summary>
        /// 设置模块或通道描述
        /// </summary>
        SettingModuleOrChannelDescription = 0X0004,

        /// <summary>
        /// 设置模块或通道图片的名称或地址
        /// </summary>
        SettingModuleOrChannelImage = 0X0005,

        /// <summary>
        /// 设置模块的时区
        /// </summary>
        SettingModuleTimezone = 0X0006,

        /// <summary>
        /// 同步时间到模块中
        /// </summary>
        SettingSyncTimeToModule = 0X0007,

        /// <summary>
        /// 设置模块或通道定时任务
        /// </summary>
        SettingModuleOrChannelTimedTask = 0X0008,

        /// <summary>
        /// 取消定时任务
        /// </summary>
        SettingCancelTimedTask = 0X0009,

        /// <summary>
        /// 设置模块型号
        /// </summary>
        SettingModuleModelNumber = 0X0010,

        /// <summary>
        /// 升级模块固件
        /// </summary>
        SettingModuleUpgradeFirmware = 0X0011,

        /// <summary>
        /// 设置模块或通道运行的模式
        /// </summary>
        SettingModuleOrChannelMode = 0X0020,

        /// <summary>
        /// 设置模块或通道运行的场景
        /// </summary>
        SettingModuleOrChannelScene = 0X0030,

        /// <summary>
        /// 取消当前运行的场景
        /// </summary>
        SettingCancelScene = 0X0031,

        /// <summary>
        /// 设置亮度传感器阈值
        /// </summary>
        SettingBrightnessSensorThreshold = 0X60,

        /// <summary>
        /// 开启或关闭红外补光功能
        /// </summary>
        SettingSwitchInfrared = 0X0070,

        /// <summary>
        /// 开启或关闭语音对讲功能
        /// </summary>
        SettingSwitchVoiceTalkback = 0X0080,

        /// <summary>
        /// 开启或关闭语音提示音功能
        /// </summary>
        SettingSwitchVoiceReminder = 0X0081,

        /// <summary>
        /// 开启或关闭语音识别功能
        /// </summary>
        SettingSwitchVoiceRecognition = 0X0082,

        /// <summary>
        /// 设置越界报警音
        /// </summary>
        SettingSlopOverWarningTone = 0X008A,

        /// <summary>
        /// 设置移动物体侦测报警音
        /// </summary>
        SettingMoveObjectWarningTone = 0X008B,

        /// <summary>
        /// 开启或关闭视频
        /// </summary>
        SettingSwitchVideo = 0X0090,

        /// <summary>
        /// 开启或关闭越界报警功能
        /// </summary>
        SettingSwitchSlopOver = 0X0091,

        /// <summary>
        /// 开启或关闭移动物体侦测功能
        /// </summary>
        SettingSwitchMoveObject = 0X0092,

        /// <summary>
        /// 开启或关闭设备分享功能
        /// </summary>
        SettingSwitchDeviceShare = 0X00A0,

        /// <summary>
        /// 设置串口波特率
        /// </summary>
        SettingModuleSerialBaud = 0X00B0,

        /// <summary>
        /// 设置串口数据位
        /// </summary>
        SettingModuleSerialDataBit = 0X00B1,

        /// <summary>
        /// 设置串口停止位
        /// </summary>
        SettingModuleSerialStopBit = 0X00B2,

        /// <summary>
        /// 设置串口校验位
        /// </summary>
        SettingModuleSerialParityBit = 0X00B3,

        /// <summary>
        /// 设置WiFi模块的运行模式
        /// </summary>
        SettingModuleWifiRunMode = 0X00C0,

        /// <summary>
        /// 设置WiFi无线网络名称SSID
        /// </summary>
        SettingModuleSsidName = 0X00C1,

        /// <summary>
        /// 设置模块的WiFi密码
        /// </summary>
        SettingModuleWifiPassword = 0X00C2,

        /// <summary>
        /// 设置模块的IP地址
        /// </summary>
        SettingModuleIpAddress = 0X00C3,

        /// <summary>
        /// 设置模块的网关地址
        /// </summary>
        SettingModuleGatewayAddress = 0X00C4,

        /// <summary>
        /// 设置模块的子网掩码
        /// </summary>
        SettingModuleSubnetMask = 0X00C5,

        /// <summary>
        /// 设置模块的DNS地址
        /// </summary>
        SettingModuleDnsAddress = 0X00C6,

        /// <summary>
        /// 设置模块的DHCP功能
        /// </summary>
        SettingModuleDhcpFunction = 0X00C7,

        /// <summary>
        /// 设置模块DHCP地址池起始地址
        /// </summary>
        SettingModuleDhcpBeginAddress = 0X00C8,

        /// <summary>
        /// 设置模块DHCP地址池结束地址
        /// </summary>
        SettingModuleDhcpEndAddress = 0X00C9,

        /// <summary>
        /// 设置模块的MAC地址
        /// </summary>
        SettingModuleMacAddress = 0X00CA,

        /// <summary>
        /// 设置模块使用的网络协议
        /// </summary>
        SettingModuleNetworkProtocol = 0X00CB,

        /// <summary>
        /// 设置模块TCP连接使用的端口
        /// </summary>
        SettingModuleTcpPort = 0X00CC,

        /// <summary>
        /// 设置模块UDP连接使用的端口
        /// </summary>
        SettingModuleUdpPort = 0X00CD,

        /// <summary>
        /// 设置模块HTTP服务器使用的端口
        /// </summary>
        SettingModuleHttpPort = 0X00CE,

        /// <summary>
        /// 设置访问服务器的IP地址
        /// </summary>
        SettingAccessServerIpAddress = 0X00D0,

        /// <summary>
        /// 设置访问服务器的域名
        /// </summary>
        SettingAccessServerDomainName = 0X00D1,

        /// <summary>
        /// 设置访问服务器的端口
        /// </summary>
        SettingAccessServerPort = 0X00D2,

        /// <summary>
        /// 设置访问服务器的用户名
        /// </summary>
        SettingAccessServerUserName = 0X00D3,

        /// <summary>
        /// 设置访问服务器的密码
        /// </summary>
        SettingAccessServerPassword = 0X00D4,

        /// <summary>
        /// 设置事件上报等待时间
        /// </summary>
        SettingEventReportWaitTime = 0X00E0,

        /// <summary>
        /// 设置事件上报重发次数
        /// </summary>
        SettingEventReportRepeatCount = 0X00E1,

        /// <summary>
        /// 设置心跳包间隔时间
        /// </summary>
        SettingHeartbeatTntervalTime = 0X00E3,

        /// <summary>
        /// 设置心跳包等待时间
        /// </summary>
        SettingHeartbeatWaitTime = 0X00E4,

        /// <summary>
        /// 设置心跳包重发次数
        /// </summary>
        SettingHeartbeatRepeatCount = 0X00E5,

        /// <summary>
        /// 模块恢复出厂设置
        ///     <para>网络恢复</para>
        /// </summary>
        SettingModuleResetFactory = 0X00FE,

        /// <summary>
        /// 设备恢复出厂设置
        ///     <para>数据恢复，慎用</para>
        /// </summary>
        SettingDeviceResetFactory = 0X00FF,
        #endregion

        #region 控制功能
        /// <summary>
        /// 定位模块或通道
        /// </summary>
        ControlModuleOrChannelLocate = 0X1000,

        /// <summary>
        /// 打开或关闭模块或通道
        /// </summary>
        ControlModuleOrChannelSwitch = 0X1001,

        /// <summary>
        /// 调节模块或通道亮度
        /// </summary>
        ControlModuleOrChannelBrightness = 0X1002,

        /// <summary>
        /// 调节模块或通道色温
        /// </summary>
        ControlModuleOrChannelColorTemperature = 0X1003,

        /// <summary>
        /// 调节模块或通道红绿蓝白（RGBW）
        /// </summary>
        ControlModuleOrChannelRgbw = 0X1004,

        /// <summary>
        /// 随机变化状态
        /// </summary>
        ControlModuleOrChannelRandom = 0X1010,

        /// <summary>
        /// 检测到敏感词
        /// </summary>
        ControlSensitiveWord = 0X1011,

        /// <summary>
        /// 发生越界报警
        /// </summary>
        ControlHappenSlopOver = 0X1090,

        /// <summary>
        /// 发生移动物体侦测报警
        /// </summary>
        ControlHappenMoveObject = 0X1091,
        #endregion

        #region 数据采集
        /// <summary>
        /// 搜索模块及通道
        /// </summary>
        StatusModuleOrChannelSearch = 0X2000,

        /// <summary>
        /// 发送心跳包到模块
        /// </summary>
        StatusHeartbeatPacketToModule = 0X2001,

        /// <summary>
        /// 获取模块或通道的打开或关闭状态
        /// </summary>
        StatusModuleOrChannelSwitch = 0X2002,

        /// <summary>
        /// 获取模块或通道的亮度等级
        /// </summary>
        StatusModuleOrChannelBrightness = 0X2003,

        /// <summary>
        /// 获取模块或通道的色温数据
        /// </summary>
        StatusModuleOrChannelColorTemperature = 0X2004,

        /// <summary>
        /// 获取模块或通道的红绿蓝白（RGBW）颜色数据
        /// </summary>
        StatusModuleOrChannelRgbw = 0X2005,

        /// <summary>
        /// 获取模块或通道的分区代码
        /// </summary>
        StatusModuleOrChannelPartitionCode = 0X2006,

        /// <summary>
        /// 获取模块或通道分区名称
        /// </summary>
        StatusModuleOrChannelPartitionName = 0X2007,

        /// <summary>
        /// 获取模块或通道的名称
        /// </summary>
        StatusModuleOrChannelName = 0X2008,

        /// <summary>
        /// 获取模块或通道的描述
        /// </summary>
        StatusModuleOrChannelDescription = 0X2009,

        /// <summary>
        /// 获取模块或通道图片名称或地址
        /// </summary>
        StatusModuleOrChannelImage = 0X200A,

        /// <summary>
        /// 获取模块或通道的定时任务
        /// </summary>
        StatusModuleOrChannelTimedTask = 0X200B,

        /// <summary>
        /// 获取模块型号
        /// </summary>
        StatusModuleModelNumber = 0X2010,

        /// <summary>
        /// 获取模块固件版本
        /// </summary>
        StatusModuleFirmwareVersion = 0X2011,

        /// <summary>
        /// 获取当前运行的模式标识
        /// </summary>
        StatusModuleModeMark = 0X2020,

        /// <summary>
        /// 获取当前运行的场景标识
        /// </summary>
        StatusModuleSceneMark = 0X2030,

        /// <summary>
        /// 获取亮度传感器检测到的环境亮度
        /// </summary>
        StatusSambientBrightness = 0X2060,

        /// <summary>
        /// 获取亮度传感器的阈值
        /// </summary>
        StatusBrightnessSensorThreshold = 0X2061,

        /// <summary>
        /// 获取模块的通信类型
        /// </summary>
        StatusModuleCommunicationType = 0X20C0,

        /// <summary>
        /// 获取WiFi模块的运行模式
        /// </summary>
        StatusModuleWifiRunMode = 0X20C1,

        /// <summary>
        /// 获取模块的无线网络名称SSID和信号强度
        /// </summary>
        StatusModuleSsidAndPower = 0X20C2,

        /// <summary>
        /// 获取模块附近的无线网络名称SSID和信号强度
        /// </summary>
        StatusModuleNearbySsidAndPower = 0X20C3,

        /// <summary>
        /// 获取模块的IP地址
        /// </summary>
        StatusModuleIpAddress = 0X20C4,

        /// <summary>
        /// 获取模块的网关地址
        /// </summary>
        StatusModuleGatewayAddress = 0x20C5,

        /// <summary>
        /// 获取模块的子网掩码
        /// </summary>
        StatusModuleSubnetMask = 0X20C6,

        /// <summary>
        /// 获取模块的DNS地址
        /// </summary>
        StatusModuleDnsAddress = 0X20C7,

        /// <summary>
        /// 获取模块的MAC地址
        /// </summary>
        StatusModuleMacAddress = 0X20C8,
        #endregion

        #region 主动请求
        /// <summary>
        /// 登录到服务器
        /// </summary>
        RequestLoginServer = 0X3000,

        /// <summary>
        /// 发送心跳包到服务器
        /// </summary>
        RequestHeartbeatPacketToServer = 0X3001,

        /// <summary>
        /// 请求服务器时间
        /// </summary>
        RequestServerTime = 0X3002,

        /// <summary>
        /// 读取摄像头时间
        /// </summary>
        RequestCameraTime = 0X3090,

        /// <summary>
        /// 读取摄像头ID
        /// </summary>
        RequestCameraIdentification = 0X3091,
        #endregion

        #region 事件上报
        /// <summary>
        /// 上报模块或通道的打开或关闭状态
        /// </summary>
        ReportModuleOrChannelSwitch = 0X4000,

        /// <summary>
        /// 上报模块或通道的亮度等级
        /// </summary>
        ReportModuleOrChannelBrightness = 0X4001,

        /// <summary>
        /// 上报模块或通道的色温数据
        /// </summary>
        ReportModuleOrChannelColorTemperature = 0X4002,

        /// <summary>
        /// 上报模块或通道的红绿蓝白（RGBW）颜色数据
        /// </summary>
        ReportModuleOrChannelRgbw = 0X4003,

        /// <summary>
        /// 上报当前运行的模式标识
        /// </summary>
        ReportModuleModeMark = 0X4020,

        /// <summary>
        /// 上报当前运行的场景标识
        /// </summary>
        ReportModuleSceneMark = 0X4030,

        /// <summary>
        /// 上报模块或通道的状态
        /// </summary>
        ReportModuleOrChannelStatus = 0X40FF,
        #endregion

        #region 其他功能
        /// <summary>
        /// 数据点功能
        /// </summary>
        DataPointFunction = 0XE000
        #endregion
    }

    /// <summary>
    /// 参数类型枚举
    /// </summary>
    public enum ParameterType : ushort
    {
        /// <summary>
        /// 无参数
        /// </summary>
        None = 0X0000,

        /// <summary>
        /// 通道数量
        /// </summary>
        ChannelCount = 0X0001,

        /// <summary>
        /// 通道编号
        /// </summary>
        ChannelNo = 0X0002,

        /// <summary>
        /// 分区代码
        /// </summary>
        PartitionCode = 0X0003,

        /// <summary>
        /// 分区名称
        /// </summary>
        PartitionName = 0X0004,

        /// <summary>
        /// 模块或通道功能
        /// </summary>
        ModuleOrChannelFunction = 0X0005,

        /// <summary>
        /// 模块或通道名称
        /// </summary>
        ModuleOrChannelName = 0X0006,

        /// <summary>
        /// 模块或通道描述
        /// </summary>
        ModuleOrChannelDescription = 0X0007,

        /// <summary>
        /// 模块或通道图片名称或地址
        /// </summary>
        ModuleOrChannelImage = 0X0008,

        /// <summary>
        /// 时区
        /// </summary>
        Timezone = 0X0009,

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime = 0X000A,

        /// <summary>
        /// 日期时间
        ///     <para>表示格林威治标准时间（GMT）1970年1月1日0时0分0秒到当前时间所间隔的秒数。</para>
        /// </summary>
        DateTime2 = 0X000B,

        /// <summary>
        /// 间隔时间，单位秒
        /// </summary>
        TntervalTime = 0X000C,

        /// <summary>
        /// 次数
        ///     <para>一个字节的标量数值。</para>
        /// </summary>
        Count = 0X000D,

        /// <summary>
        /// 序号或编号
        /// </summary>
        SerialNumber = 0X000E,

        /// <summary>
        /// 星期标识
        ///     <para>使用1个Byte的二进制形式表示，从低位到第7位分别表示周1至周日，最高位保留，全为0表示不重复。</para>
        /// </summary>
        WeekMark = 0X000F,

        /// <summary>
        /// 模块型号
        /// </summary>
        ModelNumber = 0X0010,

        /// <summary>
        /// 模式标识
        /// </summary>
        ModeMark = 0X0020,

        /// <summary>
        /// 场景标识
        /// </summary>
        SceneMark = 0X0030,

        /// <summary>
        /// 环境亮度阈值
        ///     <para>第1个字节表示白天阈值，第2个字节表示晚上阈值。</para>
        /// </summary>
        BrightnessThreshold = 0X0060,

        /// <summary>
        /// 环境亮度
        /// </summary>
        SambientBrightness = 0X0061,

        /// <summary>
        /// 红外补光开关状态
        /// </summary>
        InfraredSwitch = 0X0070,

        /// <summary>
        /// 语音对讲开关状态
        /// </summary>
        VoiceTalkbackSwitch = 0X0080,

        /// <summary>
        /// 提示音开关状态
        /// </summary>
        VoiceReminderSwitch = 0X0081,

        /// <summary>
        /// 语音识别开关状态
        /// </summary>
        VoiceRecognitionSwitch = 0X0082,

        /// <summary>
        /// 越界报警音编号
        /// </summary>
        SlopOverWarningToneNumber = 0X008A,

        /// <summary>
        /// 移动物体侦测报警音编号
        /// </summary>
        MoveObjectWarningToneNumber = 0X008B,

        /// <summary>
        /// 视频开关状态
        /// </summary>
        VideoSwitch = 0X0090,

        /// <summary>
        /// 越界报警开关状态
        /// </summary>
        SlopOverSwitch = 0X0091,

        /// <summary>
        /// 移动物体侦测开关状态
        /// </summary>
        MoveObjectSwitch = 0X0092,

        /// <summary>
        /// 摄像头ID
        /// </summary>
        CameraIdentification = 0X009A,

        /// <summary>
        /// 设备分享开关状态
        /// </summary>
        DeviceShareSwitch = 0X00A0,

        /// <summary>
        /// 串口波特率
        /// </summary>
        SerialBaud = 0X00B0,

        /// <summary>
        /// 串口数据位
        /// </summary>
        SerialDataBit = 0X00B1,

        /// <summary>
        /// 串口停止位
        /// </summary>
        SerialStopBit = 0X00B2,

        /// <summary>
        /// 串口校验位
        /// </summary>
        SerialParityBit = 0X00B3,

        /// <summary>
        /// WiFi模块的运行模式
        /// </summary>
        WifiRunMode = 0X00C0,

        /// <summary>
        /// WiFi无线网络名称SSID
        /// </summary>
        SsidName = 0X00C1,

        /// <summary>
        /// 模块的WiFi密码
        /// </summary>
        WifiPassword = 0X00C2,

        /// <summary>
        /// WiFi加密方式
        /// </summary>
        SecurityMode = 0X00C3,

        /// <summary>
        /// WiFi信号强度
        /// </summary>
        SignalPower = 0X00C4,

        /// <summary>
        /// 模块的IP地址
        /// </summary>
        IpAddress = 0X00C5,

        /// <summary>
        /// 模块的网关地址
        /// </summary>
        GatewayAddress = 0X00C6,

        /// <summary>
        /// 模块的子网掩码
        /// </summary>
        SubnetMask = 0X00C7,

        /// <summary>
        /// 模块的DNS地址
        /// </summary>
        DnsAddress = 0X00C8,

        /// <summary>
        /// 模块的DHCP功能
        /// </summary>
        DhcpFunction = 0X00C9,

        /// <summary>
        /// 模块的MAC地址
        /// </summary>
        MacAddress = 0X00CA,

        /// <summary>
        /// 模块使用的网络协议
        /// </summary>
        NetworkProtocol = 0X00CB,

        /// <summary>
        /// TCP连接使用的端口
        /// </summary>
        TcpPort = 0X00CC,

        /// <summary>
        /// UDP连接使用的端口
        /// </summary>
        UdpPort = 0X00CD,

        /// <summary>
        /// HTTP服务器使用的端口
        /// </summary>
        HttpPort = 0X00CE,

        /// <summary>
        /// 访问服务器的IP地址
        /// </summary>
        ServerIpAddress = 0X00D0,

        /// <summary>
        /// 访问服务器的域名
        /// </summary>
        ServerDomainName = 0X00D1,

        /// <summary>
        /// 访问服务器的端口
        /// </summary>
        ServerPort = 0X00D2,

        /// <summary>
        /// 访问服务器的用户名
        /// </summary>
        ServerUserName = 0X00D3,

        /// <summary>
        /// 访问服务器的密码
        /// </summary>
        ServerPassword = 0X00D4,

        /// <summary>
        /// 打开或关闭灯具
        /// </summary>
        LampSwitch = 0X1000,

        /// <summary>
        /// 亮度等级
        /// </summary>
        Brightness = 0X1001,

        /// <summary>
        /// 冷色温分量
        /// </summary>
        ColdColorTemperature = 0X1002,

        /// <summary>
        /// 红绿蓝白（RGBW）
        /// </summary>
        Rgbw = 0X1003,

        /// <summary>
        /// 冷暖色温值
        /// </summary>
        ColorTemperature = 0X1004,

        /// <summary>
        /// 亮度等级+
        /// </summary>
        BrightnessPlus = 0X2000,

        /// <summary>
        /// 亮度等级-
        /// </summary>
        BrightnessMinus = 0X2001,

        /// <summary>
        /// 冷色温分量+
        /// </summary>
        ColorTemperaturePlus = 0X2002,

        /// <summary>
        /// 冷色温分量-
        /// </summary>
        ColorTemperatureMinus = 0X2003,

        /// <summary>
        /// 布尔类型参数
        /// </summary>
        BooleanTypeParameter = 0XE000,

        /// <summary>
        /// 枚举类型参数
        /// </summary>
        EnumerationTypeParameter = 0XE001,

        /// <summary>
        /// 数字类型参数
        /// </summary>
        NumericalTypeParameter = 0XE002,

        /// <summary>
        /// 扩展类型参数
        /// </summary>
        ExtensionTypeParameter = 0XE003,

        /// <summary>
        /// 响应码
        /// </summary>
        ResponseCode = 0XFFFF
    }

    /// <summary>
    /// 设备功能枚举
    /// </summary>
    public enum DeviceFunction : byte
    {
        /// <summary>
        /// 支持时钟功能
        /// </summary>
        Timing = 0X00,

        /// <summary>
        /// 支持本地语音功控制
        /// </summary>
        LocalVoice = 0X01,

        /// <summary>
        /// 支持电话指令控制
        /// </summary>
        PhoneCommand = 0X02,

        /// <summary>
        /// 支持手机短信控制
        /// </summary>
        ShortMessage = 0X03,

        /// <summary>
        /// 可随音乐变换场景
        /// </summary>
        FollowMusic = 0X04,

        /// <summary>
        /// 可随视频变换场景
        /// </summary>
        FollowVideo = 0X05,

        /// <summary>
        /// 可随图片变换场景
        /// </summary>
        FollowImage = 0X06,

        /// <summary>
        /// 支持IFTTT功能
        /// </summary>
        SupportIfttt = 0X07,

        /// <summary>
        /// 支持HomeKit平台
        /// </summary>
        SupportHomeKit = 0X08,

        /// <summary>
        /// 支持微信设备功能
        /// </summary>
        SupportWeChat = 0X09,

        /// <summary>
        /// 支持QQ物联平台
        /// </summary>
        SupportQq = 0X0A,

        /// <summary>
        /// 支持阿里智能云物联平台
        /// </summary>
        SupportAli = 0X0B,

        /// <summary>
        /// 支持京东微联
        /// </summary>
        SupportJd = 0X0C,

        /// <summary>
        /// 支持海尔U+平台
        /// </summary>
        SupportUPlus = 0X0D,

        /// <summary>
        /// 灯具开关
        /// </summary>
        LightSwitch = 0X30,

        /// <summary>
        /// 亮度调节功能
        /// </summary>
        Brightness = 0X31,

        /// <summary>
        /// 色温调节功能
        /// </summary>
        ColorTemperature = 0X32,

        /// <summary>
        /// RGB调节功能
        /// </summary>
        LightRgb = 0X33,

        /// <summary>
        /// 摄像头开关
        /// </summary>
        CameraSwitch = 0X40,

        /// <summary>
        /// 拍照功能
        /// </summary>
        Photograph = 0X41,

        /// <summary>
        /// 录像功能
        /// </summary>
        Video = 0X42,

        /// <summary>
        /// 温度传感器
        /// </summary>
        TemperatureSensor = 0XE0,

        /// <summary>
        /// 湿度传感器
        /// </summary>
        HumiditySensor = 0XE1,

        /// <summary>
        /// PM2.5传感器
        /// </summary>
        Pm25 = 0XE5
    }

    /// <summary>
    /// 响应码
    /// </summary>
    public enum ResponseCode : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0X00,

        /// <summary>
        /// 命令格式错误
        /// </summary>
        CommandFormatError = 0X01,

        /// <summary>
        /// 消息体CRC校验错误
        /// </summary>
        CrcCheckError = 0X02,

        /// <summary>
        /// 不支持该类型的命令
        /// </summary>
        NonsupportType = 0X03,

        /// <summary>
        /// 不支持该操作
        /// </summary>
        NonsupportOperation = 0X04,

        /// <summary>
        /// 命令无法执行
        /// </summary>
        NotExecuted = 0X05,

        /// <summary>
        /// 参数个数错误
        /// </summary>
        ParameterCountError = 0X06,

        /// <summary>
        /// 参数格式错误
        /// </summary>
        ParameterFormatError = 0X07,

        /// <summary>
        /// 未知错误
        /// </summary>
        Unknown = 0XFF,
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType : byte
    {
        /// <summary>
        /// 模块
        /// </summary>
        Module,

        /// <summary>
        /// 通道
        ///     <para>即回路，每个通道对应一个子设备</para>
        /// </summary>
        Channel
    }

    /// <summary>
    /// 指定在 System.IO.Ports.SerialPort 对象上使用的据位数数。
    /// </summary>
    public enum DataBits : byte
    {
        /// <summary>
        /// 数据位长度为5
        /// </summary>
        DataBit5 = 0X05,

        /// <summary>
        /// 数据位长度为6
        /// </summary>
        DataBit6 = 0X06,

        /// <summary>
        /// 数据位长度为7
        /// </summary>
        DataBit7 = 0X07,

        /// <summary>
        /// 数据位长度为8
        /// </summary>
        DataBit8 = 0X08
    }

    /// <summary>
    /// WiFi无线网络模块的运行模式
    /// </summary>
    public enum WifiRunMode : byte
    {
        /// <summary>
        /// 服务器模式
        /// </summary>
        Server = 0X00,

        /// <summary>
        /// 客户端模式
        /// </summary>
        Client = 0X01
    }

    /// <summary>
    /// WiFi无线网络的加密方式
    /// </summary>
    public enum WifiSecurityType : byte
    {
        NONE = 0X00,
        WEP = 0X01,
        WPA_TKIP = 0X02,
        WPA_AES = 0X03,
        WPA2_TKIP = 0X04,
        WPA2_AES = 0X05,
        WPA2_MIXED = 0X06,
        AUTO = 0X07
    }

    /// <summary>
    /// WiFi模块DHCP功能
    /// </summary>
    public enum WifiDhcpFunction : byte
    {
        /// <summary>
        /// 关闭DHCP功能
        /// </summary>
        DisableDhcp = 0X00,

        /// <summary>
        /// 启用DHCP客户端
        /// </summary>
        EnableDhcp = 0X01,

        /// <summary>
        /// 创建DHCP服务器
        /// </summary>
        CreateDhcp = 0X02
    }

    /// <summary>
    /// WiFi模块支持的网络协议
    /// </summary>
    public enum NetworkProtocol : byte
    {
        /// <summary>
        /// 传输控制协议
        /// </summary>
        TCP = 0X00,

        /// <summary>
        /// 用户数据报协议
        /// </summary>
        UDP = 0X01,

        /// <summary>
        /// 超文本传输协议
        /// </summary>
        HTTP = 0X02
    }
}