using System;
using System.Collections.Generic;

namespace AirHome
{
    public class Zone
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 房间对象列表
        /// </summary>
        public List<Room> rooms { get; private set; }

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public Zone()
        {
            Name = "all";//代表所有区域的特殊区域
            rooms = new List<Room>();
        }

        /// <summary>
        /// 初始化区域
        /// </summary>
        /// <param name="name">区域名称</param>
        public Zone(string name)
        {
            Name = name;
            rooms = new List<Room>();
        }

        /// <summary>
        /// 处理更新区域名称的事件
        /// </summary>
        public event EventHandler UpdateNameCompleted;

        /// <summary>
        /// 处理添加房间的事件
        /// </summary>
        public event EventHandler AddRoomCompleted;

        /// <summary>
        /// 处理移除房间的事件
        /// </summary>
        public event EventHandler RemoveRoomCompleted;

        /// <summary>
        /// 引发处理更新区域名称的事件
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
        /// 引发处理添加房间的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAddRoom(UpdateEventArgs e)
        {
            if (this.AddRoomCompleted != null)
            {
                this.AddRoomCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理移除房间的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnRemoveRoom(UpdateEventArgs e)
        {
            if (this.RemoveRoomCompleted != null)
            {
                this.RemoveRoomCompleted(this, e);
            }
        }

        /// <summary>
        /// 更新区域名称
        /// </summary>
        /// <param name="name">区域名称</param>
        public void UpdateName(string name)
        {
            Exception ex = null;

            if (string.IsNullOrEmpty(name))
            {
                ex = new Exception("区域名称不能为空。");
            }
            else if (Name == name.Trim())
            {
                ex = new Exception("新区域名称与旧区域名称没有变化。");
            }
            else if ("all" == name.Trim())
            {
                ex = new Exception("新区域名称不能为all。");
            }
            else
            {
                Name = name.Trim();
                ex = new Exception("区域名称更新成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnUpdateName(arg);
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="room">房间对象</param>
        public void AddRoom(Room room)
        {
            Exception ex = null;

            if (room != null)
            {
                if (!string.IsNullOrEmpty(room.Name) && "all" != room.Name.Trim())
                {
                    if (rooms == null)
                    {
                        ex = new Exception("房间列表重新初始化，添加房间成功。");
                        rooms = new List<Room>();
                        rooms.Add(room);
                    }
                    else
                    {
                        if (rooms.Contains(room))
                        {
                            ex = new Exception("房间已经存在于房间列表中。");
                        }
                        else
                        {
                            ex = new Exception("添加房间成功。");
                            rooms.Add(room);
                        }
                    }
                }
                else
                {
                    ex = new Exception("房间名称不能为" + (string.IsNullOrEmpty(room.Name) ? "空" : "all") + "。");
                }
            }
            else
            {
                ex = new Exception("房间对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddRoom(arg);
        }

        /// <summary>
        /// 移除房间
        /// </summary>
        /// <param name="room">房间对象</param>
        public void RemoveRoom(Room room)
        {
            Exception ex = null;

            if (room != null)
            {
                if (!string.IsNullOrEmpty(room.Name) && "all" != room.Name.Trim())
                {
                    if (rooms == null)
                    {
                        ex = new Exception("房间列表不存在，已将其重新初始化。");
                        rooms = new List<Room>();
                    }
                    else
                    {
                        if (rooms.Remove(room))
                        {
                            ex = new Exception("从房间列表中移除房间成功。");
                        }
                        else
                        {
                            ex = new Exception("房间在房间列表中不存在。");
                        }
                    }
                }
                else
                {
                    ex = new Exception("房间名称不能为" + (string.IsNullOrEmpty(room.Name) ? "空" : "all") + "。");
                }
            }
            else
            {
                ex = new Exception("房间对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnRemoveRoom(arg);
        }
    }
}
