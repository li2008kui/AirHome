using System;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 数据采集行为类
    /// </summary>
    public class StatusAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和通道编号初始化数据采集行为类。
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
        public StatusAction(UInt64 devId = 0X0000000000000000, Byte ChannelNo = 0X00) : base(devId, ChannelNo) { }

        /// <summary>
        /// 搜索模块及通道信息的命令
        ///     <para>广播UID地址：0X0000000000000000</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelSearchCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelSearch, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 发送心跳包到模块的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusHeartbeatPacketToModuleCommand()
        {
            return GetDatagram(MessageId.StatusHeartbeatPacketToModule, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块或通道打开或关闭状态的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelSwitchCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelSwitch, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道亮度等级的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelBrightnessCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelBrightness, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道色温数据的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelColorTemperatureCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelColorTemperature, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道红绿蓝白（RGBW）颜色数据的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelRgbwCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelRgbw, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道分区代码的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelPartitionCodeCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelPartitionCode, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道分区名称的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelPartitionNameCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelPartitionName, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道名称的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelNameCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelName, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道描述的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelDescriptionCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelDescription, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道图片名称或地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleOrChannelImageCommand()
        {
            return GetDatagram(MessageId.StatusModuleOrChannelImage, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块型号的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleModelNumberCommand(Byte modelNumber)
        {
            return GetDatagram(MessageId.StatusModuleModelNumber, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块通信类型的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleCommunicationTypeCommand()
        {
            return GetDatagram(MessageId.StatusModuleCommunicationType, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取WiFi模块的运行模式的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleWifiRunModeCommand()
        {
            return GetDatagram(MessageId.StatusModuleWifiRunMode, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块的无线网络名称SSID和信号强度的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleSsidAndPowerCommand()
        {
            return GetDatagram(MessageId.StatusModuleSsidAndPower, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块附近的无线网络名称SSID和信号强度的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleNearbySsidAndPowerCommand()
        {
            return GetDatagram(MessageId.StatusModuleNearbySsidAndPower, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块IP地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleIpAddressCommand()
        {
            return GetDatagram(MessageId.StatusModuleIpAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块网关地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleGatewayAddressCommand()
        {
            return GetDatagram(MessageId.StatusModuleGatewayAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块子网掩码的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleSubnetMaskCommand()
        {
            return GetDatagram(MessageId.StatusModuleSubnetMask, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块DNS地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleDnsAddressCommand()
        {
            return GetDatagram(MessageId.StatusModuleDnsAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块MAC地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StatusModuleMacAddressCommand()
        {
            return GetDatagram(MessageId.StatusModuleMacAddress, new Parameter(ParameterType.None, 0X00));
        }
    }
}