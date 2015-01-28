using System;
using System.Net;

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
        /// 设备IP地址
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// 通过设备ID初始化设备对象
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </param>
        public Device(UInt64 devId)
        {
            DevId = devId;
            Name = devId.ToString();
        }
    }
}