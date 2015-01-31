using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 配置行为类
    /// </summary>
    public class ConfigAction : Action
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
            return GetDatagram(MessageId.SearchDevice, new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 定位设备
        /// </summary>
        /// <returns></returns>
        public Byte[] Locate()
        {
            return GetDatagram(MessageId.LocateDevice, new Parameter(ParameterType.CircuitNo, CircuitNo));
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

            return GetDatagram(MessageId.DevicePartition, pmtList);
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

            return GetDatagram(MessageId.SettingName, pmtList);
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

            return GetDatagram(MessageId.SettingDescription, pmtList);
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

            return GetDatagram(MessageId.TimedTask, pmtList);
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

            return GetDatagram(MessageId.SyncTime, pmtList);
        }
    }
}