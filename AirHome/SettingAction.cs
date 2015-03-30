using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 设置动作行为类
    /// </summary>
    public class SettingAction : AirAction
    {
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
        /// 设置模块或通道分区代码的命令
        /// </summary>
        /// <param name="partitionCode">分区代码</param>
        /// <returns></returns>
        public Byte[] SettingPartitionCommand(UInt32 partitionCode)
        {
            return GetDatagram(MessageId.SettingModuleOrChannelPartitionCode,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.PartitionCode,
                        new List<byte>{
                            0X00,
                            (Byte)(partitionCode >> 16),
                            (Byte)(partitionCode >> 8),
                            (Byte)partitionCode
                        })
                });
        }

        /// <summary>
        /// 设置模块或通道名称的命令
        /// </summary>
        /// <param name="name">模块或通道名称</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public Byte[] SettingModuleOrChannelNameCommand(string name, DeviceType deviceType)
        {
            if (deviceType == DeviceType.Module)
            {
                return GetDatagram(MessageId.SettingModuleOrChannelName,
                    new List<Parameter>{
                    new Parameter(ParameterType.ModuleOrChannelName, name)
                });
            }
            else
            {
                return GetDatagram(MessageId.SettingModuleOrChannelName,
                    new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.ModuleOrChannelName, name)
                });
            }
        }

        /// <summary>
        /// 设置设备或通道描述的命令
        /// </summary>
        /// <param name="description">设备描述</param>
        /// <returns></returns>
        public Byte[] SettingDescriptionCommand(string description)
        {
            return GetDatagram(MessageId.SettingModuleOrChannelDescription,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.ModuleOrChannelDescription, description)
                });
        }

        /// <summary>
        /// 同步时间到设备中的命令
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingSyncTimeCommand(DateTime dateTime)
        {
            UInt32 second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string hexString = second.ToString("X2").PadLeft(4, '0');

            return GetDatagram(MessageId.SettingSyncTimeToModule,
                new List<Parameter>{
                    new Parameter(ParameterType.ChannelNo, ChannelNo),
                    new Parameter(ParameterType.DateTime2, GetByteArray(hexString))
                });
        }

        /// <summary>
        /// 设置设备或通道定时任务时间或时段的命令
        /// </summary>
        /// <param name="dateTimes">日期时间对象</param>
        /// <returns></returns>
        public Byte[] SettingTimedTaskCommand(params DateTime[] dateTimes)
        {
            UInt32 second;
            string hexString = string.Empty;
            List<Parameter> pmtList = new List<Parameter>{
                new Parameter(ParameterType.ChannelNo, ChannelNo)
            };

            foreach (var dateTime in dateTimes)
            {
                second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                hexString = second.ToString("X2").PadLeft(4, '0');
                pmtList.Add(new Parameter(ParameterType.DateTime2, GetByteArray(hexString)));
            }

            return GetDatagram(MessageId.SettingModuleOrChannelTimedTask, pmtList);
        }

        /// <summary>
        /// 设备恢复出厂设置的命令
        ///     <para>该功能会清除所有数据，请慎用</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] SettingResetFactoryCommand()
        {
            return GetDatagram(MessageId.SettingResetFactory, new Parameter(ParameterType.None, 0X00));
        }
    }
}