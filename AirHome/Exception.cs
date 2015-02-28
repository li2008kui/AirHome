using System;
using System.Runtime.Serialization;

namespace ThisCoder.AirHome
{
    /// <summary>
    /// 发生非致命应用程序错误时引发的自定义异常。
    /// </summary>
    [Serializable]
    public class AirException : ApplicationException
    {
        /// <summary>
        /// 异常代码。
        /// </summary>
        public ResponseCode Code { get; private set; }

        /// <summary>
        /// 初始化 AirException 类的新实例。
        /// </summary>
        /// <param name="code">异常代码。</param>
        public AirException(ResponseCode code = ResponseCode.Succeed)
            : base()
        {
            Code = code;
        }

        /// <summary>
        /// 使用指定错误消息初始化 AirException 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="code">异常代码。</param>
        public AirException(string message, ResponseCode code = ResponseCode.Succeed)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the AirException class with
        /// serialized data.
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        /// <param name="code">异常代码。</param>
        public AirException(SerializationInfo info, StreamingContext context, ResponseCode code = ResponseCode.Succeed)
            : base(info, context)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the AirException class with
        /// a specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">解释异常原因的错误信息。</param>
        /// <param name="innerException">导致当前异常的异常。如果 innerException 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
        /// <param name="code">异常代码。</param>
        public AirException(string message, Exception innerException, ResponseCode code = ResponseCode.Succeed)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}