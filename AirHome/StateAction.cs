using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 数据采集行为类
    /// </summary>
    public class StateAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和回路编号初始化数据采集行为类。
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
        public StateAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 获取开关状态
        /// </summary>
        /// <returns></returns>
        public Byte[] SwitchState()
        {
            return GetDatagram(MessageId.StateSwitch, new Parameter(ParameterType.None, 0X00));
        }
    }
}