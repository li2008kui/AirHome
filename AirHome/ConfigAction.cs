using System;
using System.Collections.Generic;

namespace AirHome
{
    /// <summary>
    /// 配置行为类
    /// </summary>
    public class ConfigAction : Action
    {
        /// <summary>
        /// 搜索设备
        ///     <para>广播UID地址：0X0000000000000000</para>
        /// </summary>
        /// <returns></returns>
        public static Byte[] Search()
        {
            List<Byte> byteList = new List<Byte>();
            byteList.Add(0X00);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.None, byteList));

            return GetDatagram(MessageId.SearchDevice, 0X0000000000000000, pmtList);
        }

        /// <summary>
        /// 定位设备
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="circuitNo">
        /// 回路（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有回路，默认值为0X00</para>
        /// </param>
        /// <returns></returns>
        public static Byte[] Locate(UInt64 devId, Byte circuitNo = 0X00)
        {
            List<Byte> byteList = new List<Byte>();
            byteList.Add(circuitNo);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, byteList));

            return GetDatagram(MessageId.LocateDevice, devId, pmtList);
        }
    }
}