using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 控制行为类
    /// </summary>
    public class ControlAction : Action
    {
        /// <summary>
        /// 通过设备ID和回路编号初始化控制动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="circuitNo">
        /// 回路（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有回路，默认值为0X00</para>
        /// </param>
        public ControlAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 对设备进行开关操作
        ///     <para>如继电器开关或将设备亮度调到0%</para>
        /// </summary>
        /// <param name="status">
        /// 开关状态
        ///     <para>0X00表示关闭，0X01表示打开</para>
        /// </param>
        /// <param name="circuitNo">
        /// 回路（通道）编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有回路，默认值为0X00</para>
        /// </param>
        /// <returns></returns>
        public Byte[] Switch(Byte status, Byte circuitNo = 0X00)
        {
            if (status != 0X00 && status != 0X01)
            {
                throw new FormatException("开关状态参数错误！0X00表示关闭，0X01表示打开。");
            }

            List<Byte> byteList1 = new List<Byte>();
            byteList1.Add(circuitNo);

            List<Byte> byteList2 = new List<Byte>();
            byteList2.Add(status);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.CircuitNo, byteList1));
            pmtList.Add(new Parameter(ParameterType.Switch, byteList2));

            return GetDatagram(MessageId.Switch, pmtList);
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
            {
                level = 0X01;
            }

            List<Byte> byteList = new List<Byte>();
            byteList.Add(level);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Brightness, byteList));

            return GetDatagram(MessageId.Brightness, pmtList);
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
            List<Byte> byteList = new List<Byte>();
            byteList.Add(cool);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.ColorTemperature, byteList));

            return GetDatagram(MessageId.ColorTemperature, pmtList);
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
            {
                byteList.Add((Byte)(rgbw >> i));
            }

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));

            return GetDatagram(MessageId.AdjustRgbw, pmtList);
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
            List<Byte> byteList = new List<Byte>();
            byteList.Add(red);
            byteList.Add(green);
            byteList.Add(blue);
            byteList.Add(white);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));

            return GetDatagram(MessageId.AdjustRgbw, pmtList);
        }

        /// <summary>
        /// 对设备进行RGB调节操作
        ///     <para>参数值为一种 ARGB 颜色（alpha、红色、绿色、蓝色）</para>
        /// </summary>
        /// <param name="color">一种 ARGB 颜色（alpha、红色、绿色、蓝色）</param>
        /// <returns></returns>
        public Byte[] Rgbw(Color color)
        {
            List<Byte> byteList = new List<Byte>();
            byteList.Add(color.R);
            byteList.Add(color.G);
            byteList.Add(color.B);
            byteList.Add(color.A);

            List<Parameter> pmtList = new List<Parameter>();
            pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));

            return GetDatagram(MessageId.AdjustRgbw, pmtList);
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
            List<Byte> byteList;
            List<Parameter> pmtList;
            List<Byte[]> byteArrayList = new List<byte[]>();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    color = bitmap.GetPixel(x, y);
                    byteList = new List<Byte>();
                    byteList.Add(color.R);
                    byteList.Add(color.G);
                    byteList.Add(color.B);
                    byteList.Add(color.A);

                    pmtList = new List<Parameter>();
                    pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));
                    byteArrayList.Add(GetDatagram(MessageId.AdjustRgbw, pmtList));
                }
            }

            return byteArrayList;
        }
    }
}