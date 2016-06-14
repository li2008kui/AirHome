using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 动作行为抽象基类
    ///     <para>只能通过以下子类创建实例对象：</para>
    ///     <para>OperateAction：操作动作行为类</para>
    ///     <para>SettingAction：设置动作行为类</para>
    ///     <para>ControlAction：控制动作行为类</para>
    ///     <para>StatusAction：数据采集动作行为类</para>
    /// </summary>
    public abstract class AirAction
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </summary>
        public UInt64 DevId { get; set; }

        /// <summary>
        /// 通道编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道</para>
        /// </summary>
        public Byte ChannelNo { get; set; }

        /// <summary>
        /// 通过设备ID和通道编号初始化动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>通道编号默认值为0X00。</para>
        /// </summary>
        /// <param name="messageType">
        /// 消息类型
        ///     <para>消息类型枚举，默认值为ServerToDevice</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="channelNo">
        /// 通道编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        protected AirAction(MessageType messageType = MessageType.ServerToDevice, UInt64 devId = 0X0000000000000000, Byte channelNo = 0X00)
        {
            MessageType = messageType;
            DevId = devId;
            ChannelNo = channelNo;
        }

        /// <summary>
        /// 通过“消息ID”和“参数列表”获取消息报文字节数组
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="pmtList">参数列表</param>
        /// <returns></returns>
        protected Byte[] GetDatagram(MessageId msgId, List<Parameter> pmtList)
        {
            // 获取消息体对象
            MessageBody mb = new MessageBody(
                msgId,
                DevId,
                pmtList);

            // 获取消息体字节数组
            Byte[] msgBody = mb.GetBody();

            // 获取消息头对象
            MessageHead mh = new MessageHead(
                MessageType,
                (UInt16)(msgBody.Length),
                Counter.Instance.SeqNumber++,
                Crc.GetCrc(msgBody));

            // 返回消息报文字节数组
            return new Datagram(mh, mb).GetDatagram();
        }

        /// <summary>
        /// 通过“消息ID”和“参数结构体对象”获取消息报文字节数组
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="parameter">参数结构体对象</param>
        /// <returns></returns>
        protected Byte[] GetDatagram(MessageId msgId, Parameter parameter)
        {
            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(parameter);
            return GetDatagram(msgId, pmtList);
        }

        /// <summary>
        /// 通过字节数组获取字节列表
        /// </summary>
        /// <param name="byteArray">字节数组</param>
        /// <returns></returns>
        protected List<Byte> GetByteList(Byte[] byteArray)
        {
            List<Byte> byteList = new List<byte>();
            byteList.AddRange(byteArray);
            return byteList;
        }

        /// <summary>
        /// 消息报文处理委托
        /// </summary>
        /// <param name="sender">动作行为类</param>
        /// <param name="e">消息报文事件参数</param>
        public delegate void DatagramProcessHandler(object sender, DatagramEventArgs e);

        /// <summary>
        /// 消息报文处理事件
        /// </summary>
        public event DatagramProcessHandler DatagramProcess;

        /// <summary>
        /// 触发消息报文的处理事件
        /// </summary>
        /// <param name="e">消息报文事件参数</param>
        public virtual void OnDatagramProcess(DatagramEventArgs e)
        {
            if (this.DatagramProcess != null)
            {
                this.DatagramProcess(this, e);
            }
        }
    }
}