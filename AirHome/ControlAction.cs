using System;
using System.Collections.Generic;
using System.Linq;

namespace AirHome
{
    /// <summary>
    /// 控制行为类
    /// </summary>
    public class ControlAction : Action
    {
        /// <summary>
        /// 开关行为
        /// </summary>
        /// <param name="devId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="status">
        /// 开关状态
        ///     <para>0X00表示关闭，0X01表示打开</para>
        /// </param>
        /// <returns></returns>
        public static Byte[] Switch(UInt64 devId, Byte status)
        {
            if (status != 0X00 && status != 0X01)
            {
                throw new FormatException("开关状态参数错误！0X00表示关闭，0X01表示打开。");
            }

            List<Byte> bl = new List<Byte>();
            bl.Add(status);

            List<Parameter> pl = new List<Parameter>();
            pl.Add(new Parameter(ParameterType.Switch, bl));

            MessageBody mb = new MessageBody(
                MessageId.Switch,
                devId,
                pl);

            MessageHead mh = new MessageHead(
                (UInt16)(mb.GetBody().Length),
                MessageType.ServerToDevice,
                Counter.Instance.SeqNumber++,
                Crc.GetCrc(mb.GetBody()));

            return new Datagram(mh, mb).GetDatagram();
        }
    }
}