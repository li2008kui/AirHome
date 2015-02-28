using System;
using System.Collections.Generic;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 报文事件参数类。
    /// </summary>
    public class DatagramEventArgs : EventArgs
    {
        /// <summary>
        /// 消息报文对象列表。
        /// </summary>
        public List<Datagram> DatagramList { get; private set; }

        /// <summary>
        /// 初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        private DatagramEventArgs() : base() { }

        /// <summary>
        /// 通过报文字节数组初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        /// <param name="dataArray">消息报文字节数组</param>
        public DatagramEventArgs(Byte[] dataArray)
            : base()
        {
            DatagramList = Datagram.GetDatagramList(dataArray);
        }
    }
}