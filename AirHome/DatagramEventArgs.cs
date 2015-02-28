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
        /// 报文对象列表。
        /// </summary>
        public List<Datagram> DatagramList { get; private set; }

        /// <summary>
        /// 初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        private DatagramEventArgs() : base() { }

        /// <summary>
        /// 通过报文字节数组初始化 DatagramEventArgs 类的新实例。
        /// </summary>
        /// <param name="dataArray">报文字节数组</param>
        public DatagramEventArgs(Byte[] dataArray)
            : base()
        {
            List<Byte[]> byteArrayList = new List<byte[]>();
            List<Byte[]> newByteArrayList = new List<byte[]>();

            GetByteArrayList(dataArray, 0, ref byteArrayList);
            Descaping(byteArrayList, ref newByteArrayList);

            MessageHead mh = new MessageHead();
            MessageBody mb = new MessageBody();
            Datagram d = new Datagram();
            DatagramList = new List<Datagram>();

            foreach (var item in newByteArrayList)
            {
                mh.Length = (UInt16)((item[0] << 8) + item[1]);
                mh.Type = (MessageType)item[2];
                mh.SeqNumber = (UInt32)((item[3] << 24) + (item[4] << 16) + (item[5] << 8) + item[6]);
                mh.Reserved = (UInt32)((item[7] << 16) + (item[8] << 8) + item[9]);
                mh.Crc = (UInt16)((item[10] << 8) + item[11]);

                mb.MsgId = (MessageId)((item[12] << 8) + item[13]);
                mb.DevId = ((UInt64)item[14] << 56) + ((UInt64)item[15] << 48) + ((UInt64)item[16] << 40) + ((UInt64)item[17] << 32) + ((UInt64)item[18] << 24) + ((UInt64)item[19] << 16) + ((UInt64)item[20] << 8) + item[21];

                List<Parameter> pmtList = new List<Parameter>();
                GetParameterList(item, 22, ref pmtList);
                mb.PmtList = pmtList;

                d.Head = mh;
                d.Body = mb;
                DatagramList.Add(d);
            }
        }

        private static void GetParameterList(Byte[] item, int index, ref List<Parameter> pmtList)
        {
            Parameter parameter;
            List<Byte> byteList = new List<Byte>();

            for (int i = index; i < item.Length; i++)
            {
                if (item[i] == 0X00)
                {
                    parameter = new Parameter();
                    parameter.Type = (ParameterType)item[index];

                    for (int j = index + 1; j < i; j++)
                    {
                        byteList.Add(item[j]);
                    }

                    parameter.Value = byteList;
                    pmtList.Add(parameter);

                    GetParameterList(item, i + 1, ref pmtList);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        private static void Descaping(List<Byte[]> byteArrayList, ref List<Byte[]> newByteArrayList)
        {
            List<Byte> byteList;

            foreach (var item in byteArrayList)
            {
                byteList = new List<Byte>();

                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == 0X1B)
                    {
                        if (i + 1 < item.Length)
                        {
                            switch (item[i + 1])
                            {
                                case 0XE7:
                                    byteList.Add(0X02);
                                    break;
                                case 0XE8:
                                    byteList.Add(0X03);
                                    break;
                                case 0X00:
                                    byteList.Add(0X1B);
                                    break;
                                default:
                                    byteList.Add(item[i + 1]);
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        byteList.Add(item[i]);
                    }
                }

                newByteArrayList.Add(byteList.ToArray());
            }
        }

        private static void GetByteArrayList(Byte[] dataArray, int index, ref List<Byte[]> byteArrayList)
        {
            bool isStx = false;
            List<Byte> byteList = new List<Byte>();

            for (int i = index; i < dataArray.Length; i++)
            {
                if (dataArray[i] == 0X02)
                {
                    byteList = new List<Byte>();
                    isStx = true;
                }
                else if (dataArray[i] == 0X03)
                {
                    isStx = false;

                    if (byteList.Count > 0)
                    {
                        byteArrayList.Add(byteList.ToArray());
                    }

                    GetByteArrayList(dataArray, i + 1, ref byteArrayList);
                    break;
                }
                else if (isStx)
                {
                    byteList.Add(dataArray[i]);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}