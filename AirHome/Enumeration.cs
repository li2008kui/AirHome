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
        /// 设备到服务器
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
        /// 搜索设备
        /// </summary>
        ConfigSearchDevice = 0X0001,

        /// <summary>
        /// 定位设备
        /// </summary>
        ConfigLocateDevice = 0X0002,

        /// <summary>
        /// 设备分区
        /// </summary>
        ConfigDevicePartition = 0X0003,

        /// <summary>
        /// 设置名称
        /// </summary>
        ConfigSettingName = 0X0004,

        /// <summary>
        /// 设置描述
        /// </summary>
        ConfigSettingDescription = 0X0005,

        /// <summary>
        /// 设置定时任务时间
        /// </summary>
        ConfigTimedTask = 0X0006,

        /// <summary>
        /// 同步时间到设备中
        /// </summary>
        ConfigSyncTime = 0X0007,

        #region WiFi模块一键配置功能
        /// <summary>
        /// WPS功能
        ///     <para>路由器中WPS是由Wi-Fi联盟所推出的全新Wi-Fi安全防护设定(Wi-Fi Protected Setup)标准</para>
        /// </summary>
        ConfigWps = 0X00F0,

        /// <summary>
        /// EasyLink功能
        ///     <para>上海庆科开发的WiFi模块快速组网的功能</para>
        /// </summary>
        ConfigEasyLink = 0X00F1,

        /// <summary>
        /// AirKiss功能
        ///     <para>微信硬件团队开发的让WiFi模块快速组网的协议</para>
        /// </summary>
        ConfigAirKiss = 0X00F2,

        /// <summary>
        /// AirLink功能
        ///     <para>机智云配置设备上线的 Air Link 一键配置功能</para>
        /// </summary>
        ConfigAirLink = 0X00F3,

        /// <summary>
        /// SmartLink功能
        ///     <para>海尔配置设备上线的一键互联技术</para>
        /// </summary>
        ConfigSmartLink = 0X00F4,
        #endregion

        /// <summary>
        /// 恢复出厂设备
        /// </summary>
        ConfigReset = 0X00FF,
        #endregion

        #region 控制功能
        /// <summary>
        /// 打开或关闭设备
        /// </summary>
        ControlSwitch = 0X1000,

        /// <summary>
        /// 调节亮度
        /// </summary>
        ControlBrightness = 0X1001,

        /// <summary>
        /// 调节色温
        /// </summary>
        ControlColorTemperature = 0X1002,

        /// <summary>
        /// 调节红绿蓝白（RGBW）
        /// </summary>
        ControlAdjustRgbw = 0X1003,
        #endregion

        #region 数据采集
        /// <summary>
        /// 获取开关状态
        /// </summary>
        StateSwitch = 0X2000,

        /// <summary>
        /// 获取亮度等级
        /// </summary>
        StateBrightness = 0X2001,

        /// <summary>
        /// 获取色温数据
        /// </summary>
        StateColorTemperature = 0X2002,

        /// <summary>
        /// 获取红绿蓝白（RGBW）颜色数据
        /// </summary>
        StateRgbw = 0X2003,

        /// <summary>
        /// 获取设备分区编号
        /// </summary>
        StatePartition = 0X2004,

        /// <summary>
        /// 获取设备名称
        /// </summary>
        StateName = 0X2005,

        /// <summary>
        /// 获取设备描述
        /// </summary>
        StateDescription = 0X2006,

        /// <summary>
        /// 获取通信类型
        /// </summary>
        StateCommunicationType = 0X20F0,

        /// <summary>
        /// 获取设备IP地址
        /// </summary>
        StateIpAddress = 0X20F1,

        /// <summary>
        /// 获取设备MAC地址
        /// </summary>
        StateMacAddress = 0X20F2
        #endregion
    }

    /// <summary>
    /// 参数类型枚举
    /// </summary>
    public enum ParameterType : byte
    {
        /// <summary>
        /// 无参数
        /// </summary>
        None = 0X00,

        /// <summary>
        /// 回路数量
        /// </summary>
        CircuitCount = 0X01,

        /// <summary>
        /// 回路编号
        /// </summary>
        CircuitNo = 0X02,

        /// <summary>
        /// 分区编号
        /// </summary>
        PartitionNo = 0X03,

        /// <summary>
        /// 设备功能
        /// </summary>
        DeviceFunction = 0X04,

        /// <summary>
        /// 设备名称
        /// </summary>
        DeviceName = 0X05,

        /// <summary>
        /// 描述信息
        /// </summary>
        DeviceDescription = 0X06,

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime = 0X07,

        /// <summary>
        /// SSID
        /// </summary>
        Ssid = 0X08,

        /// <summary>
        /// WiFi密码
        /// </summary>
        WifiPassword = 0X09,

        /// <summary>
        /// 开关
        /// </summary>
        Switch = 0X10,

        /// <summary>
        /// 亮度
        /// </summary>
        Brightness = 0X11,

        /// <summary>
        /// 色温
        /// </summary>
        ColorTemperature = 0X12,

        /// <summary>
        /// 红绿蓝白（RGBW）
        /// </summary>
        Rgbw = 0X13,

        /// <summary>
        /// 响应码
        /// </summary>
        ResponseCode = 0XFF
    }

    /// <summary>
    /// 设备功能枚举
    /// </summary>
    public enum DeviceFunction : byte
    {
        /// <summary>
        /// 定时功能
        /// </summary>
        Timing = 0X00,

        /// <summary>
        /// 灯具开关
        /// </summary>
        LightSwitch = 0X10,

        /// <summary>
        /// 亮度调节功能
        /// </summary>
        Brightness = 0X11,

        /// <summary>
        /// 色温调节功能
        /// </summary>
        ColorTemperature = 0X12,

        /// <summary>
        /// RGB调节功能
        /// </summary>
        Rgb = 0X13,

        /// <summary>
        /// 摄像头开关
        /// </summary>
        CameraSwitch = 0X20,

        /// <summary>
        /// 拍照功能
        /// </summary>
        Photograph = 0X21,

        /// <summary>
        /// 录像功能
        /// </summary>
        Video = 0X22,

        /// <summary>
        /// 温度传感器
        /// </summary>
        TemperatureSensor = 0X50,

        /// <summary>
        /// 湿度传感器
        /// </summary>
        HumiditySensor = 0X51,

        /// <summary>
        /// PM2.5传感器
        /// </summary>
        Pm25 = 0X55
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
        /// 命令解析错误
        /// </summary>
        AnalysisError = 0X01,

        /// <summary>
        /// 消息体CRC校验错误
        /// </summary>
        CrcError = 0X02,

        /// <summary>
        /// 不支持该命令
        /// </summary>
        Nonsupport = 0X03,

        /// <summary>
        /// 命令无法执行
        /// </summary>
        NotExecuted = 0X04,

        /// <summary>
        /// 参数个数错误
        /// </summary>
        ParameterCountError = 0X05,

        /// <summary>
        /// 参数格式错误
        /// </summary>
        ParameterFormatError = 0X06,

        /// <summary>
        /// 未知错误
        /// </summary>
        Unknown = 0XFF,
    }
}