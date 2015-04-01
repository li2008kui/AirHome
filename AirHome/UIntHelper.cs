namespace System
{
    /// <summary>
    /// 无符号整数助手类
    /// </summary>
    public static class UIntHelper
    {
        /// <summary>
        /// 将16位无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">16位无符号整数</param>
        /// <param name="length">数组长度，即数组中的元素个数</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this UInt16 value, byte length = 2)
        {
            return GetByteArray(value, length, 2);
        }

        /// <summary>
        /// 将32位无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">32位无符号整数</param>
        /// <param name="length">数组长度，即数组中的元素个数</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this UInt32 value, byte length = 4)
        {
            return GetByteArray(value, length, 4);
        }

        /// <summary>
        /// 将64位无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">64位无符号整数</param>
        /// <param name="length">数组长度，即数组中的元素个数</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this UInt64 value, byte length = 8)
        {
            return GetByteArray(value, length, 8);
        }

        /// <summary>
        /// 将指定的无符号整数转换为字节数组
        /// </summary>
        /// <param name="value">无符号整数</param>
        /// <param name="length">需要的元素个数</param>
        /// <param name="maxLength">该无符号整数最多能转换的元素个数</param>
        /// <returns></returns>
        private static byte[] GetByteArray(UInt64 value, byte length, byte maxLength)
        {
            if (length < 1 || length > maxLength)
            {
                length = maxLength;
            }

            byte[] byteArray = new byte[length];

            for (int i = 0; i < length; i++)
            {
                byteArray[i] = (byte)(value >> ((length - 1 - i) * 8));
            }

            return byteArray;
        }
    }
}