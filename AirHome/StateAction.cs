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
        /// <param name="circuitNo">
        /// 通道（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        public StateAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 搜索设备及通道信息的命令
        ///     <para>广播UID地址：0X0000000000000000</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] Search()
        {
            return GetDatagram(MessageId.StateModuleOrChannelSearch, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取开关状态
        /// </summary>
        /// <returns></returns>
        public Byte[] SwitchState()
        {
            return GetDatagram(MessageId.StateModuleOrChannelSwitch, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取亮度等级
        /// </summary>
        /// <returns></returns>
        public Byte[] BrightnessState()
        {
            return GetDatagram(MessageId.StateModuleOrChannelBrightness, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取色温数据
        /// </summary>
        /// <returns></returns>
        public Byte[] ColorTemperatureState()
        {
            return GetDatagram(MessageId.StateModuleOrChannelColorTemperature, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取红绿蓝白（RGBW）颜色数据
        /// </summary>
        /// <returns></returns>
        public Byte[] RgbwState()
        {
            return GetDatagram(MessageId.StateModuleOrChannelRgbw, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取设备分区编号
        /// </summary>
        /// <returns></returns>
        public Byte[] PartitionState()
        {
            return GetDatagram(MessageId.StateModuleOrChannelPartition, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <returns></returns>
        public Byte[] DeviceName()
        {
            return GetDatagram(MessageId.StateModuleOrChannelName, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取设备描述
        /// </summary>
        /// <returns></returns>
        public Byte[] DeviceDescription()
        {
            return GetDatagram(MessageId.StateModuleOrChannelDescription, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 获取通信类型
        /// </summary>
        /// <returns></returns>
        public Byte[] CommunicationType()
        {
            return GetDatagram(MessageId.StateCommunicationType, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取设备IP地址
        /// </summary>
        /// <returns></returns>
        public Byte[] IpAddress()
        {
            return GetDatagram(MessageId.StateIpAddress, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 获取设备MAC地址
        /// </summary>
        /// <returns></returns>
        public Byte[] MacAddress()
        {
            return GetDatagram(MessageId.StateModuleMacAddress, new Parameter(ParameterType.None, 0X00));
        }
    }
}