using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 动作行为基类
    /// </summary>
    public class Action
    {
        /// <summary>
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </summary>
        public UInt64 DevId { get; set; }

        /// <summary>
        /// 初始化动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        /// </summary>
        public Action()
        {
            DevId = 0X0000000000000000;
        }

        /// <summary>
        /// 通过设备ID初始化动作行为类
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        public Action(UInt64 devId)
        {
            DevId = devId;
        }

        /// <summary>
        /// 获取消息报文字节数组
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="pmtList">参数列表</param>
        /// <returns></returns>
        protected byte[] GetDatagram(MessageId msgId, List<Parameter> pmtList)
        {
            MessageBody mb = new MessageBody(
                msgId,
                DevId,
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