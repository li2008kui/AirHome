using System;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 数据采集行为类
    /// </summary>
    public class StateAction : AirAction
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
        public StateAction(UInt64 devId = 0X0000000000000000, Byte ChannelNo = 0X00) : base(devId, ChannelNo) { }

        /// <summary>
        /// 搜索模块及通道信息的命令
        ///     <para>广播UID地址：0X0000000000000000</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelSearchCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelSearch, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 发送心跳包到模块的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateHeartbeatPacketToModuleCommand()
        {
            return GetDatagram(MessageId.StateHeartbeatPacketToModule, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块或通道打开或关闭状态的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelSwitchCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelSwitch, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道亮度等级的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelBrightnessCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelBrightness, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道色温数据的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelColorTemperatureCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelColorTemperature, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道红绿蓝白（RGBW）颜色数据的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelRgbwCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelRgbw, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道分区代码的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelPartitionCodeCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelPartitionCode, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道分区名称的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelPartitionNameCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelPartitionName, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道名称的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelNameCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelName, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道描述的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelDescriptionCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelDescription, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块或通道图片名称或地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleOrChannelImageCommand()
        {
            return GetDatagram(MessageId.StateModuleOrChannelImage, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 获取模块型号的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleModelNumberCommand(Byte modelNumber)
        {
            return GetDatagram(MessageId.StateModuleModelNumber, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块通信类型的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleCommunicationTypeCommand()
        {
            return GetDatagram(MessageId.StateModuleCommunicationType, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取WiFi模块的运行模式的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleWifiRunModeCommand()
        {
            return GetDatagram(MessageId.StateModuleWifiRunMode, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块的无线网络名称SSID和信号强度的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleSsidAndPowerCommand()
        {
            return GetDatagram(MessageId.StateModuleSsidAndPower, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块附近的无线网络名称SSID和信号强度的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleNearbySsidAndPowerCommand()
        {
            return GetDatagram(MessageId.StateModuleNearbySsidAndPower, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块IP地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleIpAddressCommand()
        {
            return GetDatagram(MessageId.StateModuleIpAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块网关地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleGatewayAddressCommand()
        {
            return GetDatagram(MessageId.StateModuleGatewayAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块子网掩码的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleSubnetMaskCommand()
        {
            return GetDatagram(MessageId.StateModuleSubnetMask, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块DNS地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleDnsAddressCommand()
        {
            return GetDatagram(MessageId.StateModuleDnsAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取模块MAC地址的命令
        /// </summary>
        /// <returns></returns>
        public Byte[] StateModuleMacAddressCommand()
        {
            return GetDatagram(MessageId.StateModuleMacAddress, new Parameter(ParameterType.None, 0X00));
        }
    }
}