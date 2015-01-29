using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
                (UInt16)(msgBody.Length),
                MessageType.ServerToDevice,
                Counter.Instance.SeqNumber++,
                Crc.GetCrc(msgBody));

            // 返回消息报文字节数组
            return new Datagram(mh, mb).GetDatagram();
        }

        /// <summary>
        /// 通过十六进制字符串获取字节数组
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        private Byte[] GetByteArray(string hexString, string separator = "")
        {
            List<Byte> byteList = new List<Byte>();

            if (!string.IsNullOrEmpty(hexString))
            {
                if (Regex.IsMatch(hexString.Replace(separator, "").Trim(), "^[0-9A-Fa-F]+$"))
                {
                    if (hexString.IndexOf(separator) >= 0)
                    {
                        foreach (var item in hexString.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            byteList.Add(Convert.ToByte(item, 16));
                        }
                    }
                    else
                    {
                        hexString = hexString.Trim();
                        hexString = ((hexString.Length % 2) != 0) ? ("0" + hexString) : hexString;

                        for (int i = 0; i < hexString.Length; i += 2)
                        {
                            byteList.Add(Convert.ToByte(hexString.Substring(i, 2), 16));
                        }
                    }
                }
                else
                {
                    byteList.Add(0X00);
                }
            }
            else
            {
                byteList.Add(0X00);
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 通过“消息ID”、“参数类型”和“参数值字节列表”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtType">参数的类型枚举值</param>
        /// <param name="pmtValueByteList">参数值字节列表</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, ParameterType pmtType, List<Byte> pmtValueByteList)
        {
            Parameter parameter = new Parameter(pmtType, pmtValueByteList);
            return Operate(msgId, parameter);
        }

        /// <summary>
        /// 通过“消息ID”、“参数类型”和“字节类型的参数值”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtType">参数的类型枚举值</param>
        /// <param name="pmtValueByte">字节类型的参数值</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, ParameterType pmtType, Byte pmtValueByte)
        {
            List<Byte> byteList = new List<Byte>();
            byteList.Add(pmtValueByte);

            Parameter parameter = new Parameter(pmtType, byteList);
            return Operate(msgId, parameter);
        }

        /// <summary>
        /// 通过“消息ID”、“参数类型”和“字符串类型的参数值”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtType">参数的类型枚举值</param>
        /// <param name="pmtValueString">字符串类型的参数值</param>
        /// <param name="isHex">字符串类型的pmtValueString参数值是否是十六进制</param>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, ParameterType pmtType, string pmtValueString, bool isHex = false, string separator = "")
        {
            Byte[] byteList = isHex ? GetByteArray(pmtValueString, separator) : Encoding.UTF8.GetBytes(pmtValueString);
            List<Byte> pmtByteListValue = new List<byte>();
            pmtByteListValue.AddRange(byteList);

            return Operate(msgId, pmtType, pmtByteListValue);
        }

        /// <summary>
        /// 通过“消息ID”和“参数结构体对象”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="parameter">参数结构体对象</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, Parameter parameter)
        {
            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(parameter);
            return GetDatagram(msgId, pmtList);
        }

        /// <summary>
        /// 通过“消息ID”和“参数结构体对象列表”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtList">参数结构体对象列表</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, List<Parameter> pmtList)
        {
            return GetDatagram(msgId, pmtList);
        }

        /// <summary>
        /// 通过“消息ID”和“键值对参数列表”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtKeyValueList">键值对参数列表</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, List<KeyValuePair<ParameterType, List<Byte>>> pmtKeyValueList)
        {
            List<Parameter> pmtList = new List<Parameter>();

            foreach (var item in pmtKeyValueList)
            {
                pmtList.Add(new Parameter(item.Key, item.Value));
            }

            return GetDatagram(msgId, pmtList);
        }
    }
}