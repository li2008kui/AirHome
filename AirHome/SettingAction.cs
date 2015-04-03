using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 设置动作行为类
    /// </summary>
    public class SettingAction : AirAction
    {
        #region 设置模块或通道基本信息
        /// <summary>
        /// 通过设备ID和通道编号初始化配置动作行为类。
        ///     <para>设备ID默认值为0X0000000000000000。</para>
        ///     <para>通道编号默认值为0X00。</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        /// <param name="ChannelNo">
        /// 通道编号
        ///     <para>取值范围：0X01~0XFF；若为0X00，则表示所有通道，默认值为0X00</para>
        /// </param>
        public SettingAction(UInt64 devId = 0X0000000000000000, Byte ChannelNo = 0X00) : base(devId, ChannelNo) { }

        /// <summary>
        /// 设置模块或通道信息的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="partitionName">分区名称</param>
        /// <param name="moduleOrChannelName">模块或通道名称</param>
        /// <param name="description">模块或通道描述</param>
        /// <param name="imageName">图片名称或地址</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelInfoCommand(UInt32 partitionCode, string partitionName, string moduleOrChannelName, string description, string imageName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                        new List<byte>{
                            0X00,
                            (Byte)(partitionCode >> 16),
                            (Byte)(partitionCode >> 8),
                            (Byte)partitionCode
                        }));
            pmtList.Add(new Parameter(ParameterType.PartitionName, partitionName));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelName, moduleOrChannelName));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelDescription, description));
            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelImage, imageName));
            return GetDatagram(MessageId.Multifunction, pmtList);
        }

        /// <summary>
        /// 设置模块或通道信息的命令
        /// </summary>
        /// <param name="moduleOrChannelInfo">模块或通道信息</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelInfoCommand(ModuleOrChannelInfo moduleOrChannelInfo, DeviceType deviceType = DeviceType.Module)
        {
            return SettingModuleOrChannelInfoCommand(
                moduleOrChannelInfo.PartitionCode,
                moduleOrChannelInfo.PartitionName,
                moduleOrChannelInfo.ModuleOrChannelName,
                moduleOrChannelInfo.Description,
                moduleOrChannelInfo.ImageName,
                deviceType);
        }

        /// <summary>
        /// 设置模块或通道分区代码和分区名称的命令
        /// </summary>
        /// <param name="partition">包含分区代码和分区名称的键/值对</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCommand(KeyValuePair<UInt32, string> partition, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                new List<byte>{
                    0X00,
                    (Byte)(partition.Key >> 16),
                    (Byte)(partition.Key >> 8),
                    (Byte)partition.Key
                }));
            pmtList.Add(new Parameter(ParameterType.PartitionName, partition.Value));
            return GetDatagram(MessageId.Multifunction, pmtList);
        }

        /// <summary>
        /// 设置模块或通道分区代码和分区名称的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="partitionName">分区名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCommand(UInt32 partitionCode, string partitionName, DeviceType deviceType = DeviceType.Module)
        {
            return SettingModuleOrChannelPartitionCommand(
                new KeyValuePair<uint, string>(partitionCode, partitionName),
                deviceType);
        }

        /// <summary>
        /// 设置模块或通道分区代码的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionCodeCommand(UInt32 partitionCode, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionCode,
                new List<byte>{
                    0X00,
                    (Byte)(partitionCode >> 16),
                    (Byte)(partitionCode >> 8),
                    (Byte)partitionCode
                }));
            return GetDatagram(MessageId.SettingModuleOrChannelPartitionCode, pmtList);
        }

        /// <summary>
        /// 设置模块或通道分区名称的命令
        /// </summary>
        /// <param name="partitionName">分区名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelPartitionNameCommand(string partitionName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.PartitionName, partitionName));
            return GetDatagram(MessageId.SettingModuleOrChannelPartitionName, pmtList);
        }

        /// <summary>
        /// 设置模块或通道名称的命令
        /// </summary>
        /// <param name="moduleOrChannelName">模块或通道名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelNameCommand(string moduleOrChannelName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelName, moduleOrChannelName));
            return GetDatagram(MessageId.SettingModuleOrChannelName, pmtList);
        }

        /// <summary>
        /// 设置模块或通道描述的命令
        /// </summary>
        /// <param name="description">模块或通道描述</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelDescriptionCommand(string description, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelDescription, description));
            return GetDatagram(MessageId.SettingModuleOrChannelDescription, pmtList);
        }

        /// <summary>
        /// 设置模块或通道图片名称或地址的命令
        /// </summary>
        /// <param name="imageName">图片名称或地址</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelImageCommand(string imageName, DeviceType deviceType = DeviceType.Module)
        {
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            pmtList.Add(new Parameter(ParameterType.ModuleOrChannelImage, imageName));
            return GetDatagram(MessageId.SettingModuleOrChannelImage, pmtList);
        }

        /// <summary>
        /// 设置模块时区的命令
        /// </summary>
        /// <param name="timeZoneInfo">时区信息对象</param>
        /// <returns></returns>
        public Byte[] SettingModuleTimezoneCommand(TimeZoneInfo timeZoneInfo)
        {
            timeZoneInfo = timeZoneInfo ?? TimeZoneInfo.Local;

            return GetDatagram(MessageId.SettingModuleTimezone,
                new List<Parameter>{
                    new Parameter(ParameterType.Timezone,
                        new byte[]{
                            (byte)(timeZoneInfo.BaseUtcOffset.Hours > 0 ? 0X00 : 0X01),
                            (byte)(timeZoneInfo.BaseUtcOffset.Hours),
                            (byte)(timeZoneInfo.BaseUtcOffset.Minutes),
                            0X00
                        })
                });
        }

        /// <summary>
        /// 同步时间到模块中的命令
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingSyncTimeToModuleCommand(DateTime dateTime)
        {
            UInt32 secondCount = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return GetDatagram(MessageId.SettingSyncTimeToModule,
                new List<Parameter>{
                    new Parameter(ParameterType.DateTime2, secondCount.ToByteArray())
                });
        }

        /// <summary>
        /// 设置模块或通道定时任务时间或时段的命令
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="dateTimes">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelTimedTaskCommand(DeviceType deviceType = DeviceType.Module, params DateTime[] dateTimes)
        {
            UInt32 secondCount;
            List<Parameter> pmtList = new List<Parameter>();

            if (deviceType == DeviceType.Channel)
            {
                pmtList.Add(new Parameter(ParameterType.ChannelNo, ChannelNo));
            }

            foreach (var dateTime in dateTimes)
            {
                secondCount = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                pmtList.Add(new Parameter(ParameterType.DateTime2, secondCount.ToByteArray()));
            }

            return GetDatagram(MessageId.SettingModuleOrChannelTimedTask, pmtList);
        }
        #endregion

        #region 设置模块串口信息
        /// <summary>
        /// 设置模块串口波特率的命令
        /// </summary>
        /// <param name="baud">波特率</param>
        /// <returns></returns>
        public Byte[] SettingModuleSerialBaudCommand(UInt32 baud)
        {
            return GetDatagram(MessageId.SettingModuleSerialBaud,
                new Parameter(ParameterType.DateTime2, baud.ToByteArray(4)));
        }

        #endregion

        /// <summary>
        /// 模块恢复出厂设置的命令
        ///     <para>该功能会清除所有数据，请慎用</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] SettingResetFactoryCommand()
        {
            return GetDatagram(MessageId.SettingResetFactory, new Parameter(ParameterType.None, 0X00));
        }
    }
}