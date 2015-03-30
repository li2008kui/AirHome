using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 设备类
    /// </summary>
    public class Device
    {
        /// <summary>
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </summary>
        public UInt64 DevId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 设备MAC地址
        /// </summary>
        public UInt64 MacAddress { get; set; }

        /// <summary>
        /// 设备分区
        ///     <para>键为分区编号，值为分区名称</para>
        /// </summary>
        public KeyValuePair<UInt32, string> Partition { get; set; }

        /// <summary>
        /// 设备IP地址
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// 通过UInt64类型的设备ID初始化设备对象
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        public Device(UInt64 devId)
        {
            DevId = devId;
            Name = devId.ToString();
            Partition = new KeyValuePair<uint, string>(0X00000000, "all");
        }

        /// <summary>
        /// 通过字符串类型的设备ID初始化设备对象
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>字符串类型，长度为8个字符</para>
        /// </param>
        public Device(string devId)
        {
            byte[] byteArray = devId.ToByteArray();
            DevId = 0X0000000000000000;

            for (int i = 0; i < byteArray.Length; i++)
            {
                DevId += (UInt64)(byteArray[byteArray.Length - (i + 1)] << (8 * i));
            }

            Name = DevId.ToString();
            Partition = new KeyValuePair<uint, string>(0X00000000, "all");
        }
    }
}