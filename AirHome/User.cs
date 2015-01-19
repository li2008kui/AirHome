namespace AirHome
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 初始化用户
        /// </summary>
        /// <param name="name">用户名称</param>
        public User(string name)
        {
            Name = name;
        }
    }
}