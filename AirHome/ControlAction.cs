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
        /// 对设备进行开关操作
        ///     <para>如继电器开关或将设备亮度调到0%</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
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

            List<Byte> byteList = new List<Byte>();
            byteList.Add(status);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Switch, byteList));

            return GetDatagram(MessageId.Switch, devId, pmtList);
        }

        /// <summary>
        /// 对设备进行调光操作
        ///     <para>若调光范围为1%~100%，需要转换为0X01~0XFF</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="level">
        /// 亮度等级
        ///     <para>范围：0X01~0XFF</para>
        /// </param>
        /// <returns></returns>
        public static Byte[] Dimming(UInt64 devId, Byte level)
        {
            if (level == 0X00)
            {
                level = 0X01;
            }

            List<Byte> byteList = new List<Byte>();
            byteList.Add(level);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Brightness, byteList));

            return GetDatagram(MessageId.Brightness, devId, pmtList);
        }

        /// <summary>
        /// 对设备进行色温调节操作
        ///     <para>参数值为冷色温分量</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="cool">
        /// 冷色温分量
        ///     <para>范围：0X01~0XFF</para>
        ///     <para>色温与亮度的关系：冷色温+暖色温=当前亮度</para>
        /// </param>
        /// <returns></returns>
        public static Byte[] Toning(UInt64 devId, Byte cool)
        {
            List<Byte> byteList = new List<Byte>();
            byteList.Add(cool);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.ColorTemperature, byteList));

            return GetDatagram(MessageId.ColorTemperature, devId, pmtList);
        }

        /// <summary>
        /// 对设备进行RGB调节操作
        ///     <para>参数值为红（R）、绿（G）、蓝（B）和白（W）的分量</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="rgbw">
        /// 红绿蓝三基色和白色的分量
        ///     <para>第1个字节表示红（R）的分量</para>
        ///     <para>第2个字节表示绿（G）的分量</para>
        ///     <para>第3个字节表示蓝（B）的分量</para>
        ///     <para>第4个字节表示白（W）的分量</para>
        /// </param>
        /// <returns></returns>
        public static Byte[] Rgbw(UInt64 devId, UInt32 rgbw)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 24; i >= 0; i -= 8)
            {
                byteList.Add((Byte)(rgbw >> i));
            }

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));

            return GetDatagram(MessageId.AdjustRgbw, devId, pmtList);
        }

        /// <summary>
        /// 获取消息报文字节数组
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="devId">设备ID</param>
        /// <param name="pmtList">参数列表</param>
        /// <returns></returns>
        private static byte[] GetDatagram(MessageId msgId, UInt64 devId, List<Parameter> pmtList)
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