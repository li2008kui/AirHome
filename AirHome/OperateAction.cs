using System;
using System.Collections.Generic;
using System.Text;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 操作行为类
    ///     <para>包括设置、控制和数据采集行为</para>
    /// </summary>
    public class OperateAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和通道编号初始化操作行为类（包括设置、控制和数据采集）。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>通道编号默认值为0X00。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="circuitNo">
        /// 通道（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        public OperateAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 通过“消息ID”、“参数类型”和“参数值字节列表”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="pmtType">参数的类型枚举值</param>
        /// <param name="pmtValueByteList">参数值字节列表</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, ParameterType pmtType, List<Byte> pmtValueByteList)
        {
            return Operate(msgId,
                new Parameter(pmtType, pmtValueByteList)
            );
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
            return Operate(msgId,
                new Parameter(pmtType,
                    new List<Byte> {
                        pmtValueByte
                    }
                )
            );
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
            Byte[] byteArray = isHex ?
                GetByteArray(pmtValueString, separator) :
                Encoding.UTF8.GetBytes(pmtValueString);
            return Operate(msgId, pmtType, GetByteList(byteArray));
        }

        /// <summary>
        /// 通过“消息ID”和“参数结构体对象”执行操作
        /// </summary>
        /// <param name="msgId">消息ID的枚举值</param>
        /// <param name="parameter">参数结构体对象</param>
        /// <returns></returns>
        public Byte[] Operate(MessageId msgId, Parameter parameter)
        {
            return GetDatagram(msgId,
                new List<Parameter> {
                    parameter
                }
            );
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
