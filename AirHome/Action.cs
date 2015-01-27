using System;
using System.Collections.Generic;

namespace AirHome
{
    /// <summary>
    /// 动作行为基类
    /// </summary>
    public class Action
    {
        /// <summary>
        /// 获取消息报文字节数组
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="pmtList">参数列表</param>
        /// <returns></returns>
        protected static byte[] GetDatagram(MessageId msgId, UInt64 devId, List<Parameter> pmtList)
        {
            MessageBody mb = new MessageBody(
                msgId,
                devId,
                pmtList);

            Byte[] msgBody = mb.GetBody();

            MessageHead mh = new MessageHead(
                (UInt16)(msgBody.Length),
                MessageType.ServerToDevice,
                Counter.Instance.SeqNumber++,
                Crc.GetCrc(msgBody));

            return new Datagram(mh, mb).GetDatagram();
        }
    }
}