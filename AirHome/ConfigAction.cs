using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 配置行为类
    /// </summary>
    public class ConfigAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和回路编号初始化配置动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>回路编号默认值为0X00。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="circuitNo">
        /// 回路（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有回路，默认值为0X00</para>
        /// </param>
        public ConfigAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 搜索设备
        ///     <para>广播UID地址：0X0000000000000000</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] Search()
        {
            return GetDatagram(MessageId.ConfigSearchDevice, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 定位设备
        /// </summary>
        /// <returns></returns>
        public Byte[] Locate()
        {
            return GetDatagram(MessageId.ConfigLocateDevice, new Parameter(ParameterType.CircuitNo, CircuitNo));
        }

        /// <summary>
        /// 设备分区
        /// </summary>
        /// <param name="partitionNo">分区编号</param>
        /// <returns></returns>
        public Byte[] Partition(UInt32 partitionNo)
        {
            List<Byte> byteList = new List<byte>();
            byteList.Add(0X00);
            byteList.Add((Byte)(partitionNo >> 16));
            byteList.Add((Byte)(partitionNo >> 8));
            byteList.Add((Byte)partitionNo);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));
            pmtList.Add(new Parameter(ParameterType.PartitionNo, byteList));

            return GetDatagram(MessageId.ConfigDevicePartition, pmtList);
        }

        /// <summary>
        /// 设置设备名称
        /// </summary>
        /// <param name="name">设备名称</param>
        /// <returns></returns>
        public Byte[] DeviceName(string name)
        {
            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));
            pmtList.Add(new Parameter(ParameterType.DeviceName, name));

            return GetDatagram(MessageId.ConfigSettingName, pmtList);
        }

        /// <summary>
        /// 设置设备描述
        /// </summary>
        /// <param name="description">设备描述</param>
        /// <returns></returns>
        public Byte[] Description(string description)
        {
            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));
            pmtList.Add(new Parameter(ParameterType.DeviceDescription, description));

            return GetDatagram(MessageId.ConfigSettingDescription, pmtList);
        }

        /// <summary>
        /// 设置定时任务时间
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] TimedTask(DateTime dateTime)
        {
            UInt32 second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string hexString = second.ToString("X2").PadLeft(4, '0');

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));
            pmtList.Add(new Parameter(ParameterType.DateTime, GetByteArray(hexString)));

            return GetDatagram(MessageId.ConfigTimedTask, pmtList);
        }

        /// <summary>
        /// 同步时间到设备中
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SyncTime(DateTime dateTime)
        {
            UInt32 second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string hexString = second.ToString("X2").PadLeft(4, '0');

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));
            pmtList.Add(new Parameter(ParameterType.DateTime, GetByteArray(hexString)));

            return GetDatagram(MessageId.ConfigSyncTime, pmtList);
        }

        /// <summary>
        /// 启用设备的WPS模式
        ///     <para>路由器中WPS是由Wi-Fi联盟所推出的全新Wi-Fi安全防护设定(Wi-Fi Protected Setup)标准</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] Wps()
        {
            return GetDatagram(MessageId.ConfigWps, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 启用设备的EasyLink模式
        ///     <para>上海庆科开发的WiFi模块快速组网的功能</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] EasyLink()
        {
            return GetDatagram(MessageId.ConfigEasyLink, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 启用设备的AirKiss模式
        ///     <para>微信硬件团队开发的让WiFi模块快速组网的协议</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] AirKiss()
        {
            return GetDatagram(MessageId.ConfigAirKiss, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 启用设备的AirLink模式
        ///     <para>机智云配置设备上线的 Air Link 一键配置功能</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] AirLink()
        {
            return GetDatagram(MessageId.ConfigAirLink, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 启用设备的SmartLink模式
        ///     <para>海尔配置设备上线的一键互联技术</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] SmartLink()
        {
            return GetDatagram(MessageId.ConfigSmartLink, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 恢复出厂设备
        ///     <para>该功能会清除所有数据，请慎用</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] Reset()
        {
            return GetDatagram(MessageId.ConfigReset, new Parameter(ParameterType.None, 0X00));
        }
    }
}