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
    }
}