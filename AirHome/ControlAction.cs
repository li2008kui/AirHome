using System;
using System.Collections.Generic;
using System.Drawing;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 控制行为类
    /// </summary>
    public class ControlAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和通道编号初始化控制动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>通道编号默认值为0X00。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="channelNo">
        /// 通道编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        public ControlAction(UInt64 devId = 0X0000000000000000, Byte channelNo = 0X00) : base(devId, channelNo) { }

        /// <summary>
        /// 定位设备
        /// </summary>
        /// <returns></returns>
        public Byte[] Locate()
        {
            return GetDatagram(MessageId.ControlModuleOrChannelLocate, new Parameter(ParameterType.ChannelNo, ChannelNo));
        }

        /// <summary>
        /// 对设备进行开关操作
        ///     <para>如继电器开关或将设备亮度调到0%</para>
        /// </summary>
        /// <param name="status">
        /// 开关状态
        ///     <para>0X00表示关闭，0X01表示打开</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Switch(Byte status)
        {
            if (status != 0X00 && status != 0X01)
                throw new FormatException("开关状态参数错误！0X00表示关闭，0X01表示打开。");

            return GetDatagram(MessageId.ControlModuleOrChannelSwitch,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.Switch, status)
            });
        }

        /// <summary>
        /// 对设备进行调光操作
        ///     <para>若调光范围为1%~100%，需要转换为0X01~0XFF</para>
        /// </summary>
        /// <param name="level">
        /// 亮度等级
        ///     <para>取值范围：0X01~0XFF</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Dimming(Byte level)
        {
            if (level == 0X00)
                level = 0X01;

            return GetDatagram(MessageId.ControlModuleOrChannelBrightness,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.Brightness, level)
            });
        }

        /// <summary>
        /// 对设备进行色温调节操作
        ///     <para>参数值为冷色温分量</para>
        /// </summary>
        /// <param name="cool">
        /// 冷色温分量
        ///     <para>取值范围：0X00~0XFF</para>
        ///     <para>色温与亮度的关系：冷色温+暖色温=当前亮度</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Toning(Byte cool)
        {
            return GetDatagram(MessageId.ControlModuleOrChannelColorTemperature,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.ColorTemperature, cool)
                });
        }

        /// <summary>
        /// 对设备进行RGBW调节操作
        ///     <para>参数值为红色、绿色、蓝色和白色的分量组合</para>
        /// </summary>
        /// <param name="rgbw">
        /// 红绿蓝三基色和白色的分量
        ///     <para>第1个字节表示红色（R）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第2个字节表示绿色（G）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第3个字节表示蓝色（B）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第4个字节表示白色（W）的分量。取值范围：0X00~0XFF</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Rgbw(UInt32 rgbw)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 24; i >= 0; i -= 8)
                byteList.Add((Byte)(rgbw >> i));

            return GetDatagram(MessageId.ControlModuleOrChannelAdjustRgbw,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.Rgbw, byteList)
                });
        }

        /// <summary>
        /// 对设备进行RGBW调节操作
        ///     <para>参数值为红色、绿色、蓝色和白色的分量</para>
        /// </summary>
        /// <param name="red">
        /// 红色（R）的分量
        ///     <para>取值范围：0X00~0XFF</para>
        /// </param>
        /// <param name="green">
        /// 绿色（G）的分量
        ///     <para>取值范围：0X00~0XFF</para>
        /// </param>
        /// <param name="blue">
        /// 蓝色（B）的分量
        ///     <para>取值范围：0X00~0XFF</para>
        /// </param>
        /// <param name="white">
        /// 白色（W）的分量
        ///     <para>取值范围：0X00~0XFF</para>
        ///     <para>如果没有白光，默认为0X00</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Rgbw(Byte red, Byte green, Byte blue, Byte white = 0X00)
        {
            return GetDatagram(MessageId.ControlModuleOrChannelAdjustRgbw,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.Rgbw,
                        new List<Byte>{
                            red,
                            green,
                            blue,
                            white
                        })
                });
        }

        /// <summary>
        /// 对设备进行RGB调节操作
        ///     <para>参数值为一种 ARGB 颜色（alpha、红色、绿色、蓝色）</para>
        /// </summary>
        /// <param name="color">一种 ARGB 颜色（alpha、红色、绿色、蓝色）</param>
        /// <returns></returns>
        public Byte[] Rgbw(Color color)
        {
            return GetDatagram(MessageId.ControlModuleOrChannelAdjustRgbw,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.Rgbw,
                        new List<Byte>{
                            color.R,
                            color.G,
                            color.B,
                            color.A
                        })
                });
        }

        /// <summary>
        /// 对设备进行RGB调节操作
        ///     <para>参数值为BMP图片</para>
        /// </summary>
        /// <param name="bitmap">BMP图片对象</param>
        /// <returns></returns>
        public List<Byte[]> Rgbw(Bitmap bitmap)
        {
            Color color;
            List<Byte[]> byteArrayList = new List<byte[]>();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    color = bitmap.GetPixel(x, y);
                    byteArrayList.Add(GetDatagram(MessageId.ControlModuleOrChannelAdjustRgbw,
                        new List<Parameter>{
                            new Parameter(ParameterType.ChannelNo, ChannelNo),
                            new Parameter(ParameterType.Rgbw,
                                new List<Byte>{
                                    color.R,
                                    color.G,
                                    color.B,
                                    color.A
                                })
                        }));
                }
            }

            return byteArrayList;
        }
    }
}