using System;

namespace AirHome
{
    /// <summary>
    /// 更新事件参数
    /// </summary>
    public class UpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 更新期间发生的异常
        /// </summary>
        private Exception Exception { get; private set; }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        private UpdateEventArgs() { }

        /// <summary>
        /// 初始化更新参数
        /// </summary>
        /// <param name="exception">更新期间发生的异常</param>
        public UpdateEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}