using System;
using System.Collections.Generic;
using System.Drawing;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 控制动作行为类
    /// </summary>
    public class ControlAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和通道编号初始化控制动作行为类。
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
        public ControlAction(MessageType messageType = MessageType.ServerToDevice, UInt64 devId = 0X0000000000000000, Byte channelNo = 0X00)
            : base(messageType, devId, channelNo) { }

        /// <summary>
        /// 定位模块或通道的命令
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelLocateCommand(DeviceType deviceType = DeviceType.Module)
        {
            return GetDatagram(MessageId.ControlModuleOrChannelLocate,
                deviceType == DeviceType.Channel ?
                new Parameter(ParameterType.ChannelNo, ChannelNo) :
                new Parameter(ParameterType.None, 0X00));
        }

        /// <summary>
        /// 打开或关闭模块或通道的命令
        ///     <para>如继电器开关或将设备亮度调到0%</para>
        /// </summary>
        /// <param name="status">
        /// 开关状态
        ///     <para>0X00表示关闭，0X01表示打开</para>
        /// </param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelSwitchCommand(Byte status, DeviceType deviceType = DeviceType.Module)
        {
            if (status > 0X01)
                status = 0X01;

            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.LampSwitch, status));
            return GetDatagram(MessageId.ControlModuleOrChannelSwitch, pmtList);
        }

        /// <summary>
        /// 调节模块或通道亮度的命令
        ///     <para>若调光范围为1%~100%，需要转换为0X01~0XFF</para>
        /// </summary>
        /// <param name="level">
        /// 亮度等级
        ///     <para>取值范围：0X01~0XFF</para>
        /// </param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelBrightnessCommand(Byte level, DeviceType deviceType = DeviceType.Module)
        {
            if (level == 0X00)
                level = 0X01;

            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.Brightness, level));
            return GetDatagram(MessageId.ControlModuleOrChannelBrightness, pmtList);
        }

        /// <summary>
        /// 调节模块或通道色温的命令
        ///     <para>参数值为冷色温分量</para>
        /// </summary>
        /// <param name="cool">
        /// 冷色温分量
        ///     <para>取值范围：0X00~0XFF</para>
        ///     <para>色温与亮度的关系：冷色温+暖色温=当前亮度</para>
        /// </param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelColorTemperatureCommand(Byte cool, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ColdColorTemperature, cool));
            return GetDatagram(MessageId.ControlModuleOrChannelColorTemperature, pmtList);
        }

        /// <summary>
        /// 调节模块或通道RGBW的命令
        ///     <para>参数值为红色、绿色、蓝色和白色的分量组合</para>
        /// </summary>
        /// <param name="rgbw">
        /// 红绿蓝三基色和白色的分量
        ///     <para>第1个字节表示红色（R）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第2个字节表示绿色（G）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第3个字节表示蓝色（B）的分量。取值范围：0X00~0XFF</para>
        ///     <para>第4个字节表示白色（W）的分量。取值范围：0X00~0XFF</para>
        /// </param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelRgbwCommand(UInt32 rgbw, DeviceType deviceType = DeviceType.Module)
        {
            List<Byte> byteList = new List<Byte>();
            List<Parameter> pmtList = new List<Parameter>();

            for (int i = 24; i >= 0; i -= 8)
                byteList.Add((Byte)(rgbw >> i));

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.Rgbw, byteList));
            return GetDatagram(MessageId.ControlModuleOrChannelRgbw, pmtList);
        }

        /// <summary>
        /// 调节模块或通道RGBW的命令
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
        ///     <para>如果没有白光，默认为0XFF</para>
        /// </param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelRgbwCommand(Byte red, Byte green, Byte blue, Byte white = 0XFF, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.Rgbw,
                new List<Byte>{
                    red,
                    green,
                    blue,
                    white
                }));
            return GetDatagram(MessageId.ControlModuleOrChannelRgbw, pmtList);
        }

        /// <summary>
        /// 调节模块或通道RGBW的命令
        ///     <para>参数值为一种 ARGB 颜色（alpha、红色、绿色、蓝色）</para>
        /// </summary>
        /// <param name="color">一种 ARGB 颜色（alpha、红色、绿色、蓝色）</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelRgbwCommand(Color color, DeviceType deviceType = DeviceType.Module)
        {
            return ControlModuleOrChannelRgbwCommand(color.R, color.G, color.B, color.A, deviceType);
        }

        /// <summary>
        /// 调节模块或通道RGBW的命令
        ///     <para>参数值为BMP图片</para>
        /// </summary>
        /// <param name="bitmap">BMP图片对象</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public IEnumerable<Byte[]> ControlModuleOrChannelRgbwCommand(Bitmap bitmap, DeviceType deviceType = DeviceType.Module)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    yield return ControlModuleOrChannelRgbwCommand(bitmap.GetPixel(x, y), deviceType);
                }
            }
        }

        /// <summary>
        /// 调节模块或通道RGBW的命令
        ///     <para>参数值为BMP图片</para>
        /// </summary>
        /// <param name="bitmap">BMP图片对象</param>
        /// <param name="x">要检索的像素的 x 坐标。</param>
        /// <param name="y">要检索的像素的 y 坐标。</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] ControlModuleOrChannelRgbwCommand(Bitmap bitmap, int x, int y, DeviceType deviceType = DeviceType.Module)
        {
            return ControlModuleOrChannelRgbwCommand(bitmap.GetPixel(x, y), deviceType);
        }
    }
}