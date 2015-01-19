using System;
using System.Collections.Generic;

namespace AirHome
{
    /// <summary>
    /// 房间类
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 房间名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 设备对象列表
        /// </summary>
        public List<Device> devices { get; private set; }

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public Room()
        {
            Name = "all";//代表所有房间的特殊房间
            devices = new List<Device>();
        }

        /// <summary>
        /// 初始化房间
        /// </summary>
        /// <param name="name">房间名称</param>
        public Room(string name)
        {
            Name = name;
            devices = new List<Device>();
        }

        /// <summary>
        /// 处理更新房间名称的事件
        /// </summary>
        public event EventHandler UpdateNameCompleted;

        /// <summary>
        /// 引发处理更新房间名称的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnUpdateName(UpdateEventArgs e)
        {
            if (this.UpdateNameCompleted != null)
            {
                this.UpdateNameCompleted(this, e);
            }
        }

        /// <summary>
        /// 更新房间名称
        /// </summary>
        /// <param name="name">房间名称</param>
        public void UpdateName(string name)
        {
            Exception ex = null;

            if (string.IsNullOrEmpty(name))
            {
                ex = new Exception("房间名称不能为空。");
            }
            else if (Name == name.Trim())
            {
                ex = new Exception("新房间名称与旧房间名称没有变化。");
            }
            else if ("all" == name.Trim())
            {
                ex = new Exception("新房间名称不能为all。");
            }
            else
            {
                Name = name.Trim();
                ex = new Exception("房间名称更新成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnUpdateName(arg);
        }
    }
}