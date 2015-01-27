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
        /// 回路（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有回路</para>
        /// </summary>
        public Byte CircuitNo { get; set; }

        /// <summary>
        /// 通过设备ID和回路编号初始化动作行为类。
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
        public Action(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00)
        {
            DevId = devId;
            CircuitNo = circuitNo;
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
            // 将回路编号添加到字节列表
            List<Byte> byteList = new List<Byte>();
            byteList.Add(CircuitNo);

            // 重新组织参数列表
            List<Parameter> tempPmtList = new List<Parameter>();
            tempPmtList.Add(new Parameter(ParameterType.CircuitNo, byteList));
            tempPmtList.AddRange(pmtList);

            // 获取消息体对象
            MessageBody mb = new MessageBody(
                msgId,
                DevId,
                tempPmtList);

            // 获取消息体字节数组
            Byte[] msgBody = mb.GetBody();

            // 获取消息头对象
            MessageHead mh = new MessageHead(
                (UInt16)(msgBody.Length),
                MessageType.ServerToDevice,
                Counter.Instance.SeqNumber++,
                Crc.GetCrc(msgBody));

            // 返回消息报文字节数组
            return new Datagram(mh, mb).GetDatagram();
        }
    }
}