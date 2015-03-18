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
        /// 设备或回路分区
        /// </summary>
        ConfigPartition = 0X0001,

        /// <summary>
        /// 设置或回路名称
        /// </summary>
        ConfigName = 0X0002,

        /// <summary>
        /// 设置或回路描述
        /// </summary>
        ConfigDescription = 0X0003,

        /// <summary>
        /// 设置设备的时区
        /// </summary>
        ConfigTimezone = 0X0004,

        /// <summary>
        /// 同步时间到设备中
        /// </summary>
        ConfigSyncTime = 0X0005,

        /// <summary>
        /// 设置设备或回路定时任务时段
        /// </summary>
        ConfigTimedTask = 0X0006,

        /// <summary>
        /// 设置串口波特率
        /// </summary>
        ConfigSerialBaud = 0X00B0,

        /// <summary>
        /// 设置串口数据位
        /// </summary>
        ConfigSerialDataBit = 0X00B1,

        /// <summary>
        /// 设置串口停止位
        /// </summary>
        ConfigSerialStopBit = 0X00B2,

        /// <summary>
        /// 设置串口校验位
        /// </summary>
        ConfigSerialCheckBit = 0X00B3,

        /// <summary>
        /// 设置WiFi模块的运行模式
        /// </summary>
        ConfigWifiRunMode = 0X00C0,

        /// <summary>
        /// 设置WiFi无线网络名称SSID
        /// </summary>
        ConfigSsidName = 0X00C1,

        /// <summary>
        /// 设置模块的WiFi密码
        /// </summary>
        ConfigWifiPassword = 0X00C2,

        /// <summary>
        /// 设置模块的IP地址
        /// </summary>
        ConfigIpAddress = 0X00C3,

        /// <summary>
        /// 设置模块的网关地址
        /// </summary>
        ConfigGatewayAddress = 0X00C4,

        /// <summary>
        /// 设置模块的子网掩码
        /// </summary>
        ConfigSubnetMask = 0X00C5,

        /// <summary>
        /// 设置模块的DNS地址
        /// </summary>
        ConfigDnsAddress = 0X00C6,

        /// <summary>
        /// 设置模块的DHCP功能
        /// </summary>
        ConfigDhcpFunction = 0X00C7,

        /// <summary>
        /// 设置DHCP地址池起始地址
        /// </summary>
        ConfigDhcpBeginAddress = 0X00C8,

        /// <summary>
        /// 设置DHCP地址池结束地址
        /// </summary>
        ConfigDhcpEndAddress = 0X00C9,

        /// <summary>
        /// 设置模块的MAC地址
        /// </summary>
        ConfigMacAddress = 0X00CA,

        /// <summary>
        /// 设置模块使用的网络协议
        /// </summary>
        ConfigNetworkProtocol = 0X00CB,

        /// <summary>
        /// 设置TCP连接使用的端口
        /// </summary>
        ConfigTcpPort = 0X00CC,

        /// <summary>
        /// 设置UDP连接使用的端口
        /// </summary>
        ConfigUdpPort = 0X00CD,

        /// <summary>
        /// 设置HTTP服务器使用的端口
        /// </summary>
        ConfigHttpPort = 0X00CE,

        /// <summary>
        /// 设置访问服务器的IP地址
        /// </summary>
        ConfigServerIpAddress = 0X00D0,

        /// <summary>
        /// 设置访问服务器的域名
        /// </summary>
        ConfigServerDomainName = 0X00D1,

        /// <summary>
        /// 设置访问服务器的端口
        /// </summary>
        ConfigServerPort = 0X00D2,

        /// <summary>
        /// 设置访问服务器的用户名
        /// </summary>
        ConfigServerUserName = 0X00D3,

        /// <summary>
        /// 设置访问服务器的密码
        /// </summary>
        ConfigServerPassword = 0X00D4,

        /// <summary>
        /// 设置事件上报等待时间
        /// </summary>
        ConfigEventReportWaitTime = 0X00E0,

        /// <summary>
        /// 设置事件上报重发次数
        /// </summary>
        ConfigEventReportRepeatCount = 0X00E1,

        /// <summary>
        /// 发送心跳包
        /// </summary>
        ConfigHeartbeatPacket = 0X00E2,

        /// <summary>
        /// 设置心跳包间隔时间
        /// </summary>
        ConfigHeartbeatTntervalTime = 0X00E3,

        /// <summary>
        /// 设置心跳包等待时间
        /// </summary>
        ConfigHeartbeatWaitTime = 0X00E4,

        /// <summary>
        /// 设置心跳包重发次数
        /// </summary>
        ConfigHeartbeatRepeatCount = 0X00E5,

        /// <summary>
        /// 恢复出厂设备
        /// </summary>
        ConfigReset = 0X00FF,
        #endregion

        #region 控制功能
        /// <summary>
        /// 定位设备或回路
        /// </summary>
        ControlLocateDevice = 0X1000,

        /// <summary>
        /// 打开或关闭设备
        /// </summary>
        ControlSwitch = 0X1001,

        /// <summary>
        /// 调节亮度
        /// </summary>
        ControlBrightness = 0X1002,

        /// <summary>
        /// 调节色温
        /// </summary>
        ControlColorTemperature = 0X1003,

        /// <summary>
        /// 调节红绿蓝白（RGBW）
        /// </summary>
        ControlAdjustRgbw = 0X1004,
        #endregion

        #region 数据采集
        /// <summary>
        /// 搜索设备及回路
        /// </summary>
        StateSearchDevice = 0X2000,

        /// <summary>
        /// 获取设备或回路的开关状态
        /// </summary>
        StateSwitch = 0X2001,

        /// <summary>
        /// 获取设备或回路的亮度等级
        /// </summary>
        StateBrightness = 0X2002,

        /// <summary>
        /// 获取设备或回路的色温数据
        /// </summary>
        StateColorTemperature = 0X2003,

        /// <summary>
        /// 获取设备或回路的红绿蓝白（RGBW）颜色数据
        /// </summary>
        StateRgbw = 0X2004,

        /// <summary>
        /// 获取设备或回路的分区
        /// </summary>
        StatePartition = 0X2005,

        /// <summary>
        /// 获取设备或回路的名称
        /// </summary>
        StateName = 0X2006,

        /// <summary>
        /// 获取设备或回路的描述
        /// </summary>
        StateDescription = 0X2007,

        /// <summary>
        /// 获取通信类型
        /// </summary>
        StateCommunicationType = 0X20C0,

        /// <summary>
        /// 获取WiFi模块的运行模式
        /// </summary>
        StateWifiRunMode = 0X20C1,

        /// <summary>
        /// 获取设备的无线网络名称SSID和信号强度
        /// </summary>
        StateSsidAndPower = 0X20C2,

        /// <summary>
        /// 获取设备附近的无线网络名称SSID和信号强度
        /// </summary>
        StateNearbySsidAndPower = 0X20C3,

        /// <summary>
        /// 获取设备的IP地址
        /// </summary>
        StateIpAddress = 0X20C4,

        /// <summary>
        /// 获取设备的网关地址
        /// </summary>
        StateGatewayAddress = 0x20C5,

        /// <summary>
        /// 获取设备的子网掩码
        /// </summary>
        StateSubnetMas = 0X20C6,

        /// <summary>
        /// 获取设备的DNS地址
        /// </summary>
        StateDnsAddress = 0X20C7,

        /// <summary>
        /// 获取设备的MAC地址
        /// </summary>
        StateMacAddress = 0X20C8,
        #endregion

        #region 主动请求
        /// <summary>
        /// 登录到服务器
        /// </summary>
        RequestLoginServer = 0X3000,

        /// <summary>
        /// 发送心跳包到服务器
        /// </summary>
        RequestSendheartbeat = 0X3001,
        #endregion

        #region 事件上报

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
        /// 回路数量
        /// </summary>
        CircuitCount = 0X0001,

        /// <summary>
        /// 回路编号
        /// </summary>
        CircuitNo = 0X0002,

        /// <summary>
        /// 分区编号
        /// </summary>
        PartitionNo = 0X0003,

        /// <summary>
        /// 设备或回路功能
        /// </summary>
        DeviceFunction = 0X0004,

        /// <summary>
        /// 设备或回路名称
        /// </summary>
        DeviceName = 0X0005,

        /// <summary>
        /// 描述或回路信息
        /// </summary>
        DeviceDescription = 0X0006,

        /// <summary>
        /// 时区
        /// </summary>
        Timezone = 0X0007,

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime = 0X0008,

        /// <summary>
        /// 日期时间
        ///     <para>表示格林威治标准时间（GMT）1970年1月1日0时0分0秒到当前时间所间隔的秒数。</para>
        /// </summary>
        DateTime2 = 0X0009,

        /// <summary>
        /// 间隔时间，单位秒
        /// </summary>
        TntervalTime = 0X000A,

        /// <summary>
        /// 次数
        ///     <para>一个字节的标量数值。</para>
        /// </summary>
        Count = 0X000B,

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
        SerialCheckBit = 0X00B3,

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
        /// 打开或关闭
        /// </summary>
        Switch = 0X1000,

        /// <summary>
        /// 亮度
        /// </summary>
        Brightness = 0X1001,

        /// <summary>
        /// 色温
        /// </summary>
        ColorTemperature = 0X1002,

        /// <summary>
        /// 红绿蓝白（RGBW）
        /// </summary>
        Rgbw = 0X1003,

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
}