using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 配置行为类
    /// </summary>
    public class ConfigAction : AirAction
    {
        /// <summary>
        /// 通过设备ID和回路编号初始化配置动作行为类。
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
        public ConfigAction(UInt64 devId = 0X0000000000000000, Byte circuitNo = 0X00) : base(devId, circuitNo) { }

        /// <summary>
        /// 设置设备或回路分区的命令
        /// </summary>
        /// <param name="partitionNo">分区编号</param>
        /// <returns></returns>
        public Byte[] ConfigPartitionCommand(UInt32 partitionNo)
        {
            return GetDatagram(MessageId.ConfigPartition,
                new List<Parameter>{
                    new Parameter(ParameterType.CircuitNo, CircuitNo),
                    new Parameter(ParameterType.PartitionNo,
                        new List<byte>{
                            0X00,
                            (Byte)(partitionNo >> 16),
                            (Byte)(partitionNo >> 8),
                            (Byte)partitionNo
                        })
                });
        }

        /// <summary>
        /// 设置设备或回路名称的命令
        /// </summary>
        /// <param name="name">设备名称</param>
        /// <returns></returns>
        public Byte[] ConfigDeviceNameCommand(string name)
        {
            return GetDatagram(MessageId.ConfigName,
                new List<Parameter>{
                    new Parameter(ParameterType.CircuitNo, CircuitNo),
                    new Parameter(ParameterType.DeviceName, name)
                });
        }

        /// <summary>
        /// 设置设备或回路描述的命令
        /// </summary>
        /// <param name="description">设备描述</param>
        /// <returns></returns>
        public Byte[] ConfigDescriptionCommand(string description)
        {
            return GetDatagram(MessageId.ConfigDescription,
                new List<Parameter>{
                    new Parameter(ParameterType.CircuitNo, CircuitNo),
                    new Parameter(ParameterType.DeviceDescription, description)
                });
        }

        /// <summary>
        /// 同步时间到设备中的命令
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns></returns>
        public Byte[] ConfigSyncTimeCommand(DateTime dateTime)
        {
            UInt32 second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string hexString = second.ToString("X2").PadLeft(4, '0');

            return GetDatagram(MessageId.ConfigSyncTime,
                new List<Parameter>{
                    new Parameter(ParameterType.CircuitNo, CircuitNo),
                    new Parameter(ParameterType.DateTime2, GetByteArray(hexString))
                });
        }

        /// <summary>
        /// 设置设备或回路定时任务时间或时段的命令
        /// </summary>
        /// <param name="dateTimes">日期时间对象</param>
        /// <returns></returns>
        public Byte[] ConfigTimedTaskCommand(params DateTime[] dateTimes)
        {
            UInt32 second;
            string hexString = string.Empty;
            List<Parameter> pmtList = new List<Parameter>{
                new Parameter(ParameterType.CircuitNo, CircuitNo)
            };

            foreach (var dateTime in dateTimes)
            {
                second = (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                hexString = second.ToString("X2").PadLeft(4, '0');
                pmtList.Add(new Parameter(ParameterType.DateTime2, GetByteArray(hexString)));
            }

            return GetDatagram(MessageId.ConfigTimedTask, pmtList);
        }

        /// <summary>
        /// 设备恢复出厂设置的命令
        ///     <para>该功能会清除所有数据，请慎用</para>
        /// </summary>
        /// <returns></returns>
        public Byte[] ConfigResetCommand()
        {
            return GetDatagram(MessageId.ConfigReset, new Parameter(ParameterType.None, 0X00));
        }
    }
}