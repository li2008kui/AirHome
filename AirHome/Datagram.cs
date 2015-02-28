using System;
using System.Collections.Generic;
using System.Text;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 报文结构体
    /// </summary>
    public struct Datagram
    {
        /// <summary>
        /// 起始符
        ///     <para>只读属性</para>
        ///     <para>值为0X02</para>
        /// </summary>
        public Byte Stx
        {
            get
            {
                return 0X02;
            }
            private set { }
        }

        /// <summary>
        /// 消息头
        ///     <para>长度为12字节</para>
        /// </summary>
        public MessageHead Head { get; set; }

        /// <summary>
        /// 消息体
        ///     <para>长度可变</para>
        /// </summary>
        public MessageBody Body { get; set; }

        /// <summary>
        /// 结束符
        ///     <para>只读属性</para>
        ///     <para>值为0X03</para>
        /// </summary>
        public Byte Etx
        {
            get
            {
                return 0X03;
            }
            private set { }
        }

        /// <summary>
        /// 通过消息头和消息体初始化报文对象实例
        /// </summary>
        /// <param name="head">
        /// 消息头
        ///     <para>长度为12字节</para>
        /// </param>
        /// <param name="body">
        /// 消息体
        ///     <para>长度可变</para>
        /// </param>
        public Datagram(MessageHead head, MessageBody body)
            : this()
        {
            Head = head;
            Body = body;
        }

        /// <summary>
        /// 获取消息报文字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetDatagram()
        {
            List<Byte> dg = new List<byte> { this.Stx };

            Byte[] head = this.Head.GetHead();
            Byte[] body = this.Body.GetBody();

            dg.AddRange(Escaping(head));
            dg.AddRange(Escaping(body));

            dg.Add(this.Etx);

            return dg.ToArray();
        }

        /// <summary>
        /// 转义特殊字符
        ///     <para>STX转义为ESC和0XE7，即02->1BE7</para>
        ///     <para>ETX转义为ESC和0XE8，即03->1BE8</para>
        ///     <para>ESC转义为ESC和0X00，即1B->1B00</para>
        /// </summary>
        /// <param name="byteArray">字节数组</param>
        /// <returns></returns>
        private static Byte[] Escaping(Byte[] byteArray)
        {
            List<Byte> byteList = new List<byte>();

            foreach (var item in byteArray)
            {
                if (item == 0X02)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0XE7);
                }
                else if (item == 0X03)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0XE8);
                }
                else if (item == 0X1B)
                {
                    byteList.Add(0X1B);
                    byteList.Add(0X00);
                }
                else
                {
                    byteList.Add(item);
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 获取消息报文十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetDatagram())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息报文字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetDatagram());
        }
    }

    /// <summary>
    /// 消息头结构体
    /// </summary>
    public struct MessageHead
    {
        /// <summary>
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public UInt16 Length { get; set; }

        /// <summary>
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </summary>
        public MessageType Type { get; set; }

        /// <summary>
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </summary>
        public UInt32 SeqNumber { get; set; }

        /// <summary>
        /// 预留字段
        ///     <para>UInt32类型，长度为3字节</para>
        /// </summary>
        public UInt32 Reserved { get; set; }

        /// <summary>
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public UInt16 Crc { get; set; }

        /// <summary>
        /// 通过“消息类型”初始化消息头对象实例
        /// </summary>
        /// <param name="type">
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        public MessageHead(MessageType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// 通过“消息体长度”、“消息序号”和“消息体CRC校验”初始化消息头对象实例
        ///     <para>默认消息类型为服务器到设备</para>
        /// </summary>
        /// <param name="length">
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </param>
        /// <param name="crc">
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageHead(UInt16 length, UInt32 seqNumber, UInt16 crc)
            : this()
        {
            Length = length;
            Type = MessageType.ServerToDevice;
            SeqNumber = seqNumber;
            Crc = crc;
        }

        /// <summary>
        /// 通过“消息体长度”、“消息类型”、“消息序号”和“消息体CRC校验”初始化消息头对象实例
        /// </summary>
        /// <param name="length">
        /// 消息体长度
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="type">
        /// 消息类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="seqNumber">
        /// 消息序号
        ///     <para>UInt32类型，长度为4个字节</para>
        /// </param>
        /// <param name="crc">
        /// 消息体CRC校验
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageHead(UInt16 length, MessageType type, UInt32 seqNumber, UInt16 crc)
            : this()
        {
            Length = length;
            Type = type;
            SeqNumber = seqNumber;
            Crc = crc;
        }

        /// <summary>
        /// 获取消息头字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetHead()
        {
            List<Byte> mh = new List<byte>();
            mh.Add((Byte)(this.Length >> 8));
            mh.Add((Byte)(this.Length));

            mh.Add((Byte)(this.Type));

            for (int i = 24; i >= 0; i -= 8)
            {
                mh.Add((Byte)(this.SeqNumber >> i));
            }

            for (int j = 0; j < 3; j++)
            {
                mh.Add(0X00);
            }

            mh.Add((Byte)(this.Crc >> 8));
            mh.Add((Byte)(this.Crc));

            return mh.ToArray();
        }

        /// <summary>
        /// 获取消息头十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetHead())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息头字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetHead());
        }
    }

    /// <summary>
    /// 消息体结构体
    /// </summary>
    public struct MessageBody
    {
        /// <summary>
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </summary>
        public MessageId MsgId { get; set; }

        /// <summary>
        /// 设备ID
        ///     <para>UInt64类型，长度为8个字节</para>
        /// </summary>
        public UInt64 DevId { get; set; }

        /// <summary>
        /// 参数列表
        ///     <para>长度可变</para>
        /// </summary>
        public List<Parameter> PmtList { get; set; }

        /// <summary>
        /// 通过“消息ID”初始化消息体对象实例
        ///     <para>默认设备ID为0X0000000000000000，即广播到所有设备</para>
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        public MessageBody(MessageId msgId)
            : this()
        {
            MsgId = msgId;
            DevId = 0X0000000000000000;
        }

        /// <summary>
        /// 通过“设备ID”初始化消息体对象实例
        ///     <para>默认消息ID为0X0000，即包含多个功能</para>
        /// </summary>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        public MessageBody(UInt64 devId)
            : this()
        {
            MsgId = MessageId.Multifunction;
            DevId = devId;
        }

        /// <summary>
        /// 通过“消息ID”和“设备ID”初始化消息体对象实例
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        public MessageBody(MessageId msgId, UInt64 devId)
            : this()
        {
            MsgId = msgId;
            DevId = devId;
        }

        /// <summary>
        /// 通过“消息ID”、“设备ID”和“参数列表”初始化消息体对象实例
        /// </summary>
        /// <param name="msgId">
        /// 消息ID
        ///     <para>UInt16类型，长度为2个字节</para>
        /// </param>
        /// <param name="devId">
        /// 设备ID
        ///     <para>UInt64类型，长度为2个字节</para>
        /// </param>
        /// <param name="pmtList">
        /// 参数列表
        ///     <para>长度可变</para>
        /// </param>
        public MessageBody(MessageId msgId, UInt64 devId, List<Parameter> pmtList)
            : this()
        {
            MsgId = msgId;
            DevId = devId;
            PmtList = pmtList;
        }

        /// <summary>
        /// 获取消息体字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetBody()
        {
            List<Byte> mb = new List<byte>{
                (Byte)((UInt16)(this.MsgId) >> 8),
                (Byte)(this.MsgId)
            };

            for (int i = 56; i >= 0; i -= 8)
            {
                mb.Add((Byte)(this.DevId >> i));
            }

            foreach (var pmt in this.PmtList ?? new List<Parameter>())
            {
                mb.AddRange(pmt.GetParameter());
            }

            return mb.ToArray();
        }

        /// <summary>
        /// 获取消息体十六进制字符串
        /// </summary>
        /// <param name="separator">
        /// 分隔符
        ///     <para>默认为空字符</para>
        /// </param>
        /// <returns></returns>
        public string ToHexString(string separator = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetBody())
            {
                sb.Append(item.ToString("X2") + separator);
            }

            return sb.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 获取消息体字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.GetBody());
        }
    }

    /// <summary>
    /// 参数结构体
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// 参数值字节列表
        ///     <para>Byte类型列表，长度可变</para>
        /// </summary>
        public List<Byte> Value { get; set; }

        /// <summary>
        /// 参数结束符
        ///     <para>只读属性</para>
        ///     <para>值为0X00</para>
        /// </summary>
        public Byte End
        {
            get
            {
                return 0X00;
            }
            private set { }
        }

        /// <summary>
        /// 通过“参数类型”和字节类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteValue">Byte类型的参数值</param>
        public Parameter(ParameterType type, Byte byteValue)
            : this()
        {
            Type = type;
            Value = new List<byte> { byteValue };
        }

        /// <summary>
        /// 通过“参数类型”和字节数组类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteArrayValue">字节数组类型的参数值</param>
        public Parameter(ParameterType type, Byte[] byteArrayValue)
            : this()
        {
            Type = type;
            Value = GetByteList(byteArrayValue);
        }

        /// <summary>
        /// 通过“参数类型”和字符串类型的“参数值”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="stringValue">字符串类型的参数值</param>
        public Parameter(ParameterType type, string stringValue)
            : this()
        {
            Type = type;
            Value = GetByteList(Encoding.UTF8.GetBytes(stringValue));
        }

        /// <summary>
        /// 通过“参数类型”和“参数值字节列表”初始化参数对象实例
        /// </summary>
        /// <param name="type">
        /// 参数类型
        ///     <para>Byte类型，长度为1个字节</para>
        /// </param>
        /// <param name="byteValueList">
        /// 参数值字节列表
        ///     <para>Byte类型列表，长度可变</para>
        /// </param>
        public Parameter(ParameterType type, List<Byte> byteValueList)
            : this()
        {
            Type = type;
            Value = byteValueList;
        }

        /// <summary>
        /// 获取参数字节数组
        /// </summary>
        /// <returns></returns>
        public Byte[] GetParameter()
        {
            List<Byte> pmt = new List<byte> { (Byte)(this.Type) };

            if (this.Value != null && this.Value.Count > 0)
            {
                pmt.AddRange(this.Value);
            }
            else
            {
                pmt.Add(0X00);
            }

            pmt.Add(this.End);
            return pmt.ToArray();
        }

        /// <summary>
        /// 通过字节数组获取字节列表
        /// </summary>
        /// <param name="byteArray">字节数组</param>
        /// <returns></returns>
        private List<Byte> GetByteList(Byte[] byteArray)
        {
            List<Byte> byteList = new List<byte>();
            byteList.AddRange(byteArray);
            return byteList;
        }
    }
}