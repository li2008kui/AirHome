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
            List<Byte> byteList = new List<Byte>();
            byteList.Add(0X00);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.None, byteList));

            return GetDatagram(MessageId.SearchDevice, pmtList);
        }

        /// <summary>
        /// 定位设备
        /// </summary>
        /// <returns></returns>
        public Byte[] Locate()
        {
            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, CircuitNo));

            return GetDatagram(MessageId.LocateDevice, pmtList);
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
    }
}