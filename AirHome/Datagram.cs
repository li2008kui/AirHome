using System;
using System.Collections.Generic;
using System.Text;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 报文结构体
    /// </summary>
    public struct Datagram
    {
        /// <summary>
        /// 起始符
        ///     <para>只读属性</para>
        ///     <para>值为0X02</para>
        /// </summary>
        public Byte Stx
        {
            get
            {
                return 0X02;
            }
            private set { }
        }

        /// <summary>
        /// 消息头
        ///     <para>长度为12字节</para>
        /// </summary>
        public MessageHead Head { get; set; }

        /// <summary>
        /// 消息体
        ///     <para>长度可变</para>
        /// </summary>
        public MessageBody Body { get; set; }

        /// <summary>
        /// 结束符
        ///     <para>只读属性</para>
        ///     <para>值为0X03</para>
        /// </summary>
        public Byte Etx
        {
            get
            {
                return 0X03;
            }
            private set { }
        }

        /// <summary>
        /// 通过消息头和消息体初始化报文对象实例
        /// </summary>
        /// <param name="head">
        /// 消息头
        ///     <para>长度为12字节</para>
        /// </param>
        /// <param name="body">
        /// 消息体
        ///     <para>长度可变</para>
        /// </param>
        public Datagram(MessageHead head, MessageBody body)
            : this()
        {
            Head = head;
            Body = body;
        }

        /// <summary>
        /// 获取消息报文字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetDatagram()
        {
            List<Byte> dg = new List<byte>();
            dg.Add(this.Stx);

            Byte[] head = this.Head.GetHead();
            Byte[] body = this.Body.GetBody();

            dg.AddRange(Escaping(head));
            dg.AddRange(Escaping(body));

            dg.Add(this.Etx);

            return dg.ToArray();
        }

        /// <summary>
        /// 转义特殊字符
        ///     <para>STX转义为ESC和0XE7，即02->1BE7</para>
        ///     <para>ETX转义为ESC和0XE8，即03->1BE8</para>
        ///     <para>ESC转义为ESC和0X00，即1B->1B00</para>
        /// </summary>
        /// <param name="byteArray">字节数组</param>
        /// <returns></returns>
        private static Byte[] Escaping(Byte[] byteArray)
        {
            List<Byte> byteList = new List<byte>();

            foreach (var item in byteArray)
            {
                if (item == 0X02)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0XE7);
                }
                else if (item == 0X03)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0XE8);
                }
                else if (item == 0X1B)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0X00);
                }
                else
                {
                    byteList.Add(item);
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 获取消息报文十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetDatagram())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息报文字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetDatagram());
        }
    }

    /// <summary>
    /// 消息头结构体
    /// </summary>
    public struct MessageHead
    {
        /// <summary>
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public UInt16 Length { get; set; }

        /// <summary>
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </summary>
        public UInt32 SeqNumber { get; set; }

        /// <summary>
        /// 预留字段
        ///     <para>UInt32类型，长度为3字节</para>
        /// </summary>
        public UInt32 Reserved { get; set; }

        /// <summary>
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public UInt16 Crc { get; set; }

        /// <summary>
        /// 通过“消息类型”初始化消息头对象实例
        /// </summary>
        /// <param name="type">
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        public MessageHead(MessageType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// 通过“消息体长度”、“消息序号”和“消息体CRC校验”初始化消息头对象实例
        ///     <para>默认消息类型为服务器到设备</para>
        /// </summary>
        /// <param name="length">
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </param>
        /// <param name="crc">
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageHead(UInt16 length, UInt32 seqNumber, UInt16 crc)
            : this()
        {
            Length = length;
            Type = MessageType.ServerToDevice;
            SeqNumber = seqNumber;
            Crc = crc;
        }

        /// <summary>
        /// 通过“消息体长度”、“消息类型”、“消息序号”和“消息体CRC校验”初始化消息头对象实例
        /// </summary>
        /// <param name="length">
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="type">
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </param>
        /// <param name="crc">
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageHead(UInt16 length, MessageType type, UInt32 seqNumber, UInt16 crc)
            : this()
        {
            Length = length;
            Type = type;
            SeqNumber = seqNumber;
            Crc = crc;
        }

        /// <summary>
        /// 获取消息头字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetHead()
        {
            List<Byte> mh = new List<byte>();
            mh.Add((Byte)(this.Length >> 8));
            mh.Add((Byte)(this.Length));

            mh.Add((Byte)(this.Type));

            for (int i = 24; i >= 0; i -= 8)
            {
                mh.Add((Byte)(this.SeqNumber >> i));
            }

            for (int j = 0; j < 3; j++)
            {
                mh.Add(0X00);
            }

            mh.Add((Byte)(this.Crc >> 8));
            mh.Add((Byte)(this.Crc));

            return mh.ToArray();
        }

        /// <summary>
        /// 获取消息头十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetHead())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息头字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetHead());
        }
    }

    /// <summary>
    /// 消息体结构体
    /// </summary>
    public struct MessageBody
    {
        /// <summary>
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public MessageId MsgId { get; set; }

        /// <summary>
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </summary>
        public UInt64 DevId { get; set; }

        /// <summary>
        /// 参数列表
        ///     <para>长度可变</para>
        /// </summary>
        public List<Parameter> PmtList { get; set; }

        /// <summary>
        /// 通过“消息ID”初始化消息体对象实例
        ///     <para>默认设备ID为0X0000000000000000，即广播到所有设备</para>
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageBody(MessageId msgId)
            : this()
        {
            MsgId = msgId;
            DevId = 0X0000000000000000;
        }

        /// <summary>
        /// 通过“设备ID”初始化消息体对象实例
        ///     <para>默认消息ID为0X0000，即包含多个功能</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        public MessageBody(UInt64 devId)
            : this()
        {
            MsgId = MessageId.Multifunction;
            DevId = devId;
        }

        /// <summary>
        /// 通过“消息ID”和“设备ID”初始化消息体对象实例
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        public MessageBody(MessageId msgId, UInt64 devId)
            : this()
        {
            MsgId = msgId;
            DevId = devId;
        }

        /// <summary>
        /// 通过“消息ID”、“设备ID”和“参数列表”初始化消息体对象实例
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        /// <param name="pmtList">
        /// 参数列表
        ///     <para>长度可变</para>
        /// </param>
        public MessageBody(MessageId msgId, UInt64 devId, List<Parameter> pmtList)
            : this()
        {
            MsgId = msgId;
            DevId = devId;
            PmtList = pmtList;
        }

        /// <summary>
        /// 获取消息体字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetBody()
        {
            List<Byte> mb = new List<byte>();
            mb.Add((Byte)((UInt16)(this.MsgId) >> 8));
            mb.Add((Byte)(this.MsgId));

            for (int i = 56; i >= 0; i -= 8)
            {
                mb.Add((Byte)(this.DevId >> i));
            }

            foreach (var pmt in this.PmtList)
            {
                mb.AddRange(pmt.GetParameter());
            }

            return mb.ToArray();
        }

        /// <summary>
        /// 获取消息体十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetBody())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息体字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetBody());
        }
    }

    /// <summary>
    /// 参数结构体
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// 参数值字节列表
        ///     <para>Byte类型列表，长度可变</para>
        /// </summary>
        public List<Byte> Value { get; set; }

        /// <summary>
        /// 参数结束符
        ///     <para>只读属性</para>
        ///     <para>值为0X00</para>
        /// </summary>
        public Byte End
        {
            get
            {
                return 0X00;
            }
            private set { }
        }

        /// <summary>
        /// 通过“参数类型”和字节类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteValue">Byte类型的参数值</param>
        public Parameter(ParameterType type, Byte byteValue)
            : this()
        {
            List<Byte> byteList = new List<byte>();
            byteList.Add(byteValue);

            Type = type;
            Value = byteList;
        }

        /// <summary>
        /// 通过“参数类型”和字节数组类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteArrayValue">字节数组类型的参数值</param>
        public Parameter(ParameterType type, Byte[] byteArrayValue)
            : this()
        {
            List<Byte> byteList = new List<byte>();
            byteList.AddRange(byteArrayValue);

            Type = type;
            Value = byteList;
        }

        /// <summary>
        /// 通过“参数类型”和字符串类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="stringValue">字符串类型的参数值</param>
        public Parameter(ParameterType type, string stringValue)
            : this()
        {
            List<Byte> byteList = new List<byte>();
            byteList.AddRange(Encoding.UTF8.GetBytes(stringValue));

            Type = type;
            Value = byteList;
        }

        /// <summary>
        /// 通过“参数类型”和“参数值字节列表”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteValueList">
        /// 参数值字节列表
        ///     <para>Byte类型列表，长度可变</para>
        /// </param>
        public Parameter(ParameterType type, List<Byte> byteValueList)
            : this()
        {
            Type = type;
            Value = byteValueList;
        }

        /// <summary>
        /// 获取参数字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetParameter()
        {
            List<Byte> pmt = new List<byte>();
            pmt.Add((Byte)(this.Type));

            if (this.Value != null && this.Value.Count > 0)
            {
                pmt.AddRange(this.Value);
            }
            else
            {
                pmt.Add(0X00);
            }

            pmt.Add(this.End);
            return pmt.ToArray();
        }
    }

    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MessageType
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
    public enum MessageId
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
        /// 获取打关状态
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
    public enum ParameterType
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
    public enum DeviceFunction
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
    public enum ResponseCode
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