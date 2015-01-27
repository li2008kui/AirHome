using System;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 消息序号计数器类
    ///     <para>此类只有一个对象</para>
    /// </summary>
    public sealed class Counter
    {
        /// <summary>
        /// 延迟初始化对象
        /// </summary>
        private static readonly Lazy<Counter> lazy = new Lazy<Counter>(() => new Counter());

        /// <summary>
        /// 通过静态字段获取消息序号计数器实例
        /// </summary>
        public static Counter Instance { get { return lazy.Value; } }

        /// <summary>
        /// 私有默认构造方法
        /// </summary>
        private Counter() { }

        /// <summary>
        /// 消息序号
        /// </summary>
        public UInt32 SeqNumber { get; set; }
    }
}