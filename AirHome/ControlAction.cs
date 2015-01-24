using System;
using System.Linq;

namespace AirHome
{
    /// <summary>
    /// 控制行为类
    /// </summary>
    public class ControlAction : Action
    {
        /// <summary>
        /// 开关行为
        /// </summary>
        /// <param name="devId">设备ID</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public Byte[] Switch(UInt64 devId, params Parameter[] parameters)
        {
            Datagram dg = new Datagram();

            MessageBody mb = new MessageBody();
            mb.MsgId = MessageId.Switch;
            mb.DevId = devId;
            mb.PmtList = parameters.ToList();

            MessageHead mh = new MessageHead();
            mh.Length = (UInt16)(mb.GetBody().Length);
            mh.Type = MessageType.ServerToDevice;
            mh.SeqNumber = 0X00000000;
            mh.Reserved = 0X00000000000000;
            mh.Crc = Crc.GetCrc(mb.GetBody());

            dg.Head = mh;
            dg.Body = mb;
            return dg.GetDatagram();
        }
    }
}