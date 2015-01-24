using System;
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
        /// <param name="seqNumber">
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </param>
        /// <param name="devId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public Byte[] Switch(UInt32 seqNumber, UInt64 devId, params Parameter[] parameters)
        {
            MessageBody mb = new MessageBody(
                MessageId.Switch,
                devId,
                parameters.ToList());
            MessageHead mh = new MessageHead(
                (UInt16)(mb.GetBody().Length),
                MessageType.ServerToDevice,
                seqNumber,
                Crc.GetCrc(mb.GetBody()));
            return new Datagram(mh, mb).GetDatagram();
        }
    }
}