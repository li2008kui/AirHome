using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 设置动作行为类
    /// </summary>
    public class SettingAction : AirAction
    {
        #region 设置模块或通道基本信息
        /// <summary>
        /// 通过设备ID和通道编号初始化配置动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>通道编号默认值为0X00。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="ChannelNo">
        /// 通道编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        public SettingAction(UInt64 devId = 0X0000000000000000, Byte ChannelNo = 0X00) : base(devId, ChannelNo) { }

        /// <summary>
        /// 设置模块或通道信息的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="partitionName">分区名称</param>
        /// <param name="moduleOrChannelName">模块或通道名称</param>
        /// <param name="description">模块或通道描述</param>
        /// <param name="imageName">图片名称或地址</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelInfoCommand(UInt32 partitionCode, string partitionName, string moduleOrChannelName, string description, string imageName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                        new List<byte>{
                            0X00,
                            (Byte)(partitionCode >> 16),
                            (Byte)(partitionCode >> 8),
                            (Byte)partitionCode
                        }));
            pmtList.Add(new Parameter(ParameterType.PartitionName, partitionName));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelName, moduleOrChannelName));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelDescription, description));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelImage, imageName));
            return GetDatagram(MessageId.Multifunction, pmtList);
        }

        /// <summary>
        /// 设置模块或通道信息的命令
        /// </summary>
        /// <param name="moduleOrChannelInfo">模块或通道信息</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelInfoCommand(ModuleOrChannelInfo moduleOrChannelInfo, DeviceType deviceType = DeviceType.Module)
        {
            return SettingModuleOrChannelInfoCommand(
                moduleOrChannelInfo.PartitionCode,
                moduleOrChannelInfo.PartitionName,
                moduleOrChannelInfo.ModuleOrChannelName,
                moduleOrChannelInfo.Description,
                moduleOrChannelInfo.ImageName,
                deviceType);
        }

        /// <summary>
        /// 设置模块或通道分区代码和分区名称的命令
        /// </summary>
        /// <param name="partition">包含分区代码和分区名称的键/值对</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCommand(KeyValuePair<UInt32, string> partition, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                new List<byte>{
                    0X00,
                    (Byte)(partition.Key >> 16),
                    (Byte)(partition.Key >> 8),
                    (Byte)partition.Key
                }));
            pmtList.Add(new Parameter(ParameterType.PartitionName, partition.Value));
            return GetDatagram(MessageId.Multifunction, pmtList);
        }

        /// <summary>
        /// 设置模块或通道分区代码和分区名称的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="partitionName">分区名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCommand(UInt32 partitionCode, string partitionName, DeviceType deviceType = DeviceType.Module)
        {
            return SettingModuleOrChannelPartitionCommand(
                new KeyValuePair<uint, string>(partitionCode, partitionName),
                deviceType);
        }

        /// <summary>
        /// 设置模块或通道分区代码的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCodeCommand(UInt32 partitionCode, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                new List<byte>{
                    0X00,
                    (Byte)(partitionCode >> 16),
                    (Byte)(partitionCode >> 8),
                    (Byte)partitionCode
                }));
            return GetDatagram(MessageId.SettingModuleOrChannelPartitionCode, pmtList);
        }

        /// <summary>
        /// 设置模块或通道分区名称的命令
        /// </summary>
        /// <param name="partitionName">分区名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionNameCommand(string partitionName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionName, partitionName));
            return GetDatagram(MessageId.SettingModuleOrChannelPartitionName, pmtList);
        }

        /// <summary>
        /// 设置模块或通道名称的命令
        /// </summary>
        /// <param name="moduleOrChannelName">模块或通道名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelNameCommand(string moduleOrChannelName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelName, moduleOrChannelName));
            return GetDatagram(MessageId.SettingModuleOrChannelName, pmtList);
        }

        /// <summary>
        /// 设置模块或通道描述的命令
        /// </summary>
        /// <param name="description">模块或通道描述</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelDescriptionCommand(string description, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelDescription, description));
            return GetDatagram(MessageId.SettingModuleOrChannelDescription, pmtList);
        }

        /// <summary>
        /// 设置模块或通道图片名称或地址的命令
        /// </summary>
        /// <param name="imageName">图片名称或地址</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelImageCommand(string imageName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelImage, imageName));
            return GetDatagram(MessageId.SettingModuleOrChannelImage, pmtList);
        }

        /// <summary>
        /// 设置模块时区的命令
        /// </summary>
        /// <param name="timeZoneInfo">时区信息对象</param>
        /// <returns></returns>
        public Byte[] SettingModuleTimezoneCommand(TimeZoneInfo timeZoneInfo)
        {
            timeZoneInfo = timeZoneInfo ?? TimeZoneInfo.Local;

            return GetDatagram(MessageId.SettingModuleTimezone,
                new List<Parameter>{
                    new Parameter(ParameterType.Timezone,
                        new byte[]{
                            (byte)(timeZoneInfo.BaseUtcOffset.Hours > 0 ? 0X00 : 0X01),
                            (byte)(timeZoneInfo.BaseUtcOffset.Hours),
                            (byte)(timeZoneInfo.BaseUtcOffset.Minutes),
                            0X00
                        })
                });
        }

        /// <summary>
        /// 同步时间到模块中的命令
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingSyncTimeToModuleCommand(DateTime dateTime)
        {
            UInt32 secondCount = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return GetDatagram(MessageId.SettingSyncTimeToModule,
                new List<Parameter>{
                    new Parameter(ParameterType.DateTime2, secondCount.ToByteArray())
                });
        }

        /// <summary>
        /// 设置模块或通道定时任务时间或时段的命令
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="dateTimes">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelTimedTaskCommand(DeviceType deviceType = DeviceType.Module, params DateTime[] dateTimes)
        {
            UInt32 secondCount;
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            foreach (var dateTime in dateTimes)
            {
                secondCount = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                pmtList.Add(new Parameter(ParameterType.DateTime2, secondCount.ToByteArray()));
            }

            return GetDatagram(MessageId.SettingModuleOrChannelTimedTask, pmtList);
        }
        #endregion

        #region 设置模块串口信息
        /// <summary>
        /// 设置模块串口信息的命令
        /// </summary>
        /// <param name="baud">波特率</param>
        /// <param name="dataBit">数据位</param>
        /// <param name="stopBit">停止位</param>
        /// <param name="parityBit">校验位</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialInfoCommand(UInt32 baud, DataBits dataBit, StopBits stopBit, Parity parityBit)
        {
            return GetDatagram(MessageId.Multifunction,
                new List<Parameter>{
                    new Parameter(ParameterType.SerialBaud, baud.ToByteArray(3)),
                    new Parameter(ParameterType.SerialDataBit, (Byte)dataBit),
                    new Parameter(ParameterType.SerialStopBit, (Byte)stopBit),
                    new Parameter(ParameterType.SerialParityBit,(Byte)parityBit)
                });
        }

        /// <summary>
        /// 设置模块串口信息的命令
        /// </summary>
        /// <param name="serialPort">串口对象</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialInfoCommand(SerialPort serialPort)
        {
            return SettingModuleSerialInfoCommand(
                (UInt32)(serialPort.BaudRate),
                (DataBits)(serialPort.DataBits),
                serialPort.StopBits,
                serialPort.Parity);
        }

        /// <summary>
        /// 设置模块串口波特率的命令
        /// </summary>
        /// <param name="baud">波特率</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialBaudCommand(UInt32 baud)
        {
            return GetDatagram(MessageId.SettingModuleSerialBaud,
                new Parameter(ParameterType.SerialBaud, baud.ToByteArray(3)));
        }

        /// <summary>
        /// 设置模块串口数据位的命令
        /// </summary>
        /// <param name="dataBit">数据位</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialDataBitCommand(Byte dataBit)
        {
            return GetDatagram(MessageId.SettingModuleSerialDataBit,
                new Parameter(ParameterType.SerialDataBit, dataBit));
        }

        /// <summary>
        /// 设置模块串口停止位的命令
        /// </summary>
        /// <param name="stopBit">停止位</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialStopBitCommand(Byte stopBit)
        {
            return GetDatagram(MessageId.SettingModuleSerialStopBit,
                new Parameter(ParameterType.SerialStopBit, stopBit));
        }

        /// <summary>
        /// 设置模块串口校验位的命令
        /// </summary>
        /// <param name="parityBit">校验位</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialParityBitCommand(Byte parityBit)
        {
            return GetDatagram(MessageId.SettingModuleSerialParityBit,
                new Parameter(ParameterType.SerialParityBit, parityBit));
        }
        #endregion

        #region 设置WiFi模块信息
        /// <summary>
        /// 设置WiFi模块运行模式的命令
        /// </summary>
        /// <param name="wifiRunMode">WiFi模块运行模式</param>
        /// <returns></returns>
        public Byte[] SettingModuleWifiRunModeCommand(WifiRunMode wifiRunMode)
        {
            return GetDatagram(MessageId.SettingModuleWifiRunMode,
                new Parameter(ParameterType.WifiRunMode, (Byte)wifiRunMode));
        }

        /// <summary>
        /// 设置WiFi无线网络名称SSID的命令
        /// </summary>
        /// <param name="ssidName">WiFi无线网络名称SSID</param>
        /// <returns></returns>
        public Byte[] SettingModuleSsidNameCommand(string ssidName)
        {
            return GetDatagram(MessageId.SettingModuleSsidName,
                new Parameter(ParameterType.SsidName, ssidName));
        }

        /// <summary>
        /// 设置模块WiFi密码的命令
        /// </summary>
        /// <param name="wifiPassword">模块的WiFi密码</param>
        /// <returns></returns>
        public Byte[] SettingModuleWifiPasswordCommand(string wifiPassword)
        {
            return GetDatagram(MessageId.SettingModuleWifiPassword,
                new Parameter(ParameterType.WifiPassword, wifiPassword));
        }

        /// <summary>
        /// 设置模块IP地址的命令
        /// </summary>
        /// <param name="ipAddress">模块的IP地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleIpAddressCommand(string ipAddress)
        {
            return GetDatagram(MessageId.SettingModuleIpAddress,
                new Parameter(ParameterType.IpAddress, ipAddress));
        }

        /// <summary>
        /// 设置模块网关地址的命令
        /// </summary>
        /// <param name="gatewayAddress">模块的网关地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleGatewayAddressCommand(string gatewayAddress)
        {
            return GetDatagram(MessageId.SettingModuleGatewayAddress,
                new Parameter(ParameterType.GatewayAddress, gatewayAddress));
        }

        /// <summary>
        /// 设置模块子网掩码的命令
        /// </summary>
        /// <param name="subnetMask">模块的子网掩码</param>
        /// <returns></returns>
        public Byte[] SettingModuleSubnetMaskCommand(string subnetMask)
        {
            return GetDatagram(MessageId.SettingModuleSubnetMask,
                new Parameter(ParameterType.SubnetMask, subnetMask));
        }

        /// <summary>
        /// 设置模块DNS地址的命令
        /// </summary>
        /// <param name="dnsAddress">模块的DNS地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleDnsAddressCommand(string dnsAddress)
        {
            return GetDatagram(MessageId.SettingModuleDnsAddress,
                new Parameter(ParameterType.DnsAddress, dnsAddress));
        }

        /// <summary>
        /// 设置模块DHCP功能的命令
        /// </summary>
        /// <param name="wifiDhcpFunction">模块的DHCP功能</param>
        /// <returns></returns>
        public Byte[] SettingModuleDhcpFunctionCommand(WifiDhcpFunction wifiDhcpFunction)
        {
            return GetDatagram(MessageId.SettingModuleDhcpFunction,
                new Parameter(ParameterType.DhcpFunction, (Byte)wifiDhcpFunction));
        }

        /// <summary>
        /// 设置模块DHCP地址池起始地址的命令
        /// </summary>
        /// <param name="ipAddress">模块DHCP地址池起始IP地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleDhcpBeginAddressCommand(string ipAddress)
        {
            return GetDatagram(MessageId.SettingModuleDhcpBeginAddress,
                new Parameter(ParameterType.IpAddress, ipAddress));
        }

        /// <summary>
        /// 设置模块DHCP地址池结束地址的命令
        /// </summary>
        /// <param name="ipAddress">模块DHCP地址池结束IP地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleDhcpEndAddressCommand(string ipAddress)
        {
            return GetDatagram(MessageId.SettingModuleDhcpEndAddress,
                new Parameter(ParameterType.IpAddress, ipAddress));
        }

        /// <summary>
        /// 设置模块MAC地址的命令
        /// </summary>
        /// <param name="macAddress">模块的MAC地址</param>
        /// <returns></returns>
        public Byte[] SettingModuleMacAddressCommand(string macAddress)
        {
            return GetDatagram(MessageId.SettingModuleMacAddress,
                new Parameter(ParameterType.MacAddress, macAddress));
        }

        /// <summary>
        /// 设置模块使用网络协议的命令
        /// </summary>
        /// <param name="networkProtocol">模块使用的网络协议</param>
        /// <returns></returns>
        public Byte[] SettingModuleNetworkProtocolCommand(NetworkProtocol networkProtocol)
        {
            return GetDatagram(MessageId.SettingModuleNetworkProtocol,
                new Parameter(ParameterType.NetworkProtocol, (Byte)networkProtocol));
        }

        /// <summary>
        /// 设置TCP连接使用的端口的命令
        /// </summary>
        /// <param name="tcpPort">TCP连接使用的端口</param>
        /// <returns></returns>
        public Byte[] SettingModuleTcpPortCommand(UInt16 tcpPort)
        {
            return GetDatagram(MessageId.SettingModuleTcpPort,
                new Parameter(ParameterType.TcpPort, tcpPort.ToByteArray()));
        }

        /// <summary>
        /// 设置UDP连接使用的端口的命令
        /// </summary>
        /// <param name="udpPort">UDP连接使用的端口</param>
        /// <returns></returns>
        public Byte[] SettingModuleUdpPortCommand(UInt16 udpPort)
        {
            return GetDatagram(MessageId.SettingModuleUdpPort,
                new Parameter(ParameterType.UdpPort, udpPort.ToByteArray()));
        }

        /// <summary>
        /// 设置设置HTTP服务器使用的端口的命令
        /// </summary>
        /// <param name="httpPort">
        ///     设置HTTP服务器使用的端口。
        ///     <para>默认为80端口</para>
        /// </param>
        /// <returns></returns>
        public Byte[] SettingModuleHttpPortCommand(UInt16 httpPort = 0X50)
        {
            return GetDatagram(MessageId.SettingModuleHttpPort,
                new Parameter(ParameterType.HttpPort, httpPort.ToByteArray()));
        }
        #endregion

        #region 设置模块访问服务器信息

        #endregion

        #region 设置事件上报及心跳参数

        #endregion

        #region 设置其他参数信息
        /// <summary>
        /// 模块恢复出厂设置的命令
        ///     <para>该功能会清除所有数据，请慎用</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] SettingResetFactoryCommand()
        {
            return GetDatagram(MessageId.SettingResetFactory, new Parameter(ParameterType.None, 0X00));
        }
        #endregion
    }
}