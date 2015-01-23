using System;
using System.Collections.Generic;

namespace AirHome
{
    /// <summary>
    /// 住所主类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 住所名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 是否是主要住所
        /// </summary>
        public bool IsPrimary { get; private set; }

        /// <summary>
        /// 无参构造方法
        ///     <para>住所名称为NULL</para>
        ///     <para>默认为主要住所</para>
        /// </summary>
        public House()
        {
            IsPrimary = true;
            devices = new List<Device>();
            users = new List<User>();
            rooms = new List<Room>();
            zones = new List<Zone>();
        }

        /// <summary>
        /// 初始化住所
        /// </summary>
        /// <param name="name">住所名称</param>
        ///     <para>默认为主要住所</para>
        /// <param name="isPrimary">是否是主要住所</param>
        public House(string name, bool isPrimary = true)
        {
            Name = name;
            IsPrimary = isPrimary;
            devices = new List<Device>();
            users = new List<User>();
            rooms = new List<Room>();
            zones = new List<Zone>();
        }

        /// <summary>
        /// 处理更新住所名称的事件
        /// </summary>
        public event EventHandler UpdateNameCompleted;
        /// <summary>
        /// 处理使住所成为主要住所的事件
        /// </summary>
        public event EventHandler BecomePrimaryCompleted;

        /// <summary>
        /// 引发处理更新住所名称的事件
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
        /// 引发处理使住所成为主要住所的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnBecomePrimary(UpdateEventArgs e)
        {
            if (this.BecomePrimaryCompleted != null)
            {
                this.BecomePrimaryCompleted(this, e);
            }
        }

        /// <summary>
        /// 更新住所名称
        /// </summary>
        /// <param name="name">住所名称</param>
        public void UpdateName(string name)
        {
            Exception ex = null;

            if (string.IsNullOrEmpty(name))
            {
                ex = new Exception("住所名称不能为空。");
            }
            else if (Name == name.Trim())
            {
                ex = new Exception("新住所名称与旧住所名称没有变化。");
            }
            else
            {
                Name = name;
                ex = new Exception("住所名称更新成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnUpdateName(arg);
        }

        /// <summary>
        /// 使住所成为主要住所
        /// </summary>
        public void BecomePrimary()
        {
            Exception ex = null;
            string name = "[" + (Name ?? "null") + "]";

            if (IsPrimary)
            {
                ex = new Exception(name + "已经是主要住所。");
            }
            else
            {
                IsPrimary = true;
                ex = new Exception("设置" + name + "为主要住所成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnBecomePrimary(arg);
        }
    }

    /// <summary>
    /// 住所设备分部类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 设备对象列表
        /// </summary>
        public List<Device> devices { get; private set; }

        /// <summary>
        /// 处理添加设备的事件
        /// </summary>
        public event EventHandler AddDeviceCompleted;

        /// <summary>
        /// 处理移除设备的事件
        /// </summary>
        public event EventHandler RemoveDeviceCompleted;

        /// <summary>
        /// 处理分配设备到房间的事件
        /// </summary>
        public event EventHandler AssignDeviceToRoomCompleted;

        /// <summary>
        /// 引发处理添加设备的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAddDevice(UpdateEventArgs e)
        {
            if (this.AddDeviceCompleted != null)
            {
                this.AddDeviceCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理移除设备的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnRemoveDevice(UpdateEventArgs e)
        {
            if (this.RemoveDeviceCompleted != null)
            {
                this.RemoveDeviceCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理分配设备到房间的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAssignDeviceToRoom(UpdateEventArgs e)
        {
            if (this.AssignDeviceToRoomCompleted != null)
            {
                this.AssignDeviceToRoomCompleted(this, e);
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="device">设备对象</param>
        public void AddDevice(Device device)
        {
            Exception ex = null;

            if (device != null)
            {
                if (devices == null)
                {
                    ex = new Exception("设备列表重新初始化，添加设备成功。");
                    devices = new List<Device>();
                    devices.Add(device);
                }
                else
                {
                    if (devices.IndexOf(device) >= 0)
                    {
                        ex = new Exception("设备已经存在于设备列表中。");
                    }
                    else
                    {
                        ex = new Exception("添加设备成功。");
                        devices.Add(device);
                    }
                }
            }
            else
            {
                ex = new Exception("设备对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddDevice(arg);
        }

        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="device">设备对象</param>
        public void RemoveDevice(Device device)
        {
            Exception ex = null;

            if (device != null)
            {
                if (devices == null)
                {
                    ex = new Exception("设备列表不存在，已将其重新初始化。");
                    devices = new List<Device>();
                }
                else
                {
                    if (devices.Remove(device))
                    {
                        ex = new Exception("从设备列表中移除设备成功。");
                    }
                    else
                    {
                        ex = new Exception("设备在设备列表中不存在。");
                    }
                }
            }
            else
            {
                ex = new Exception("设备对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnRemoveDevice(arg);
        }

        /// <summary>
        /// 分配设备到房间
        /// </summary>
        /// <param name="device">设备对象</param>
        /// <param name="room">房间对象</param>
        public void AssignDeviceToRoom(Device device, Room room)
        {
            Exception ex = null;

            if (device != null)
            {
                if (room != null)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ex = new Exception("房间对象不能为空。");
                }
            }
            else
            {
                ex = new Exception("设备对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAssignDeviceToRoom(arg);
        }
    }

    /// <summary>
    /// 住所用户分部类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 用户对象列表
        /// </summary>
        public List<User> users { get; private set; }

        /// <summary>
        /// 处理添加用户的事件
        /// </summary>
        public event EventHandler AddUserCompleted;

        /// <summary>
        /// 处理移除用户的事件
        /// </summary>
        public event EventHandler RemoveUserCompleted;

        /// <summary>
        /// 引发处理添加用户的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAddUser(UpdateEventArgs e)
        {
            if (this.AddUserCompleted != null)
            {
                this.AddUserCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理移除用户的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnRemoveUser(UpdateEventArgs e)
        {
            if (this.RemoveUserCompleted != null)
            {
                this.RemoveUserCompleted(this, e);
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        public void AddUser(User user)
        {
            Exception ex = null;

            if (user != null)
            {
                if (users == null)
                {
                    ex = new Exception("用户列表重新初始化，添加用户成功。");
                    users = new List<User>();
                    users.Add(user);
                }
                else
                {
                    if (users.IndexOf(user) >= 0)
                    {
                        ex = new Exception("用户已经存在于用户列表中。");
                    }
                    else
                    {
                        ex = new Exception("添加用户成功。");
                        users.Add(user);
                    }
                }
            }
            else
            {
                ex = new Exception("用户对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddUser(arg);
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="user">用户对象</param>
        public void RemoveUser(User user)
        {
            Exception ex = null;

            if (user != null)
            {
                if (users == null)
                {
                    ex = new Exception("用户列表不存在，已将其重新初始化。");
                    users = new List<User>();
                }
                else
                {
                    if (users.Remove(user))
                    {
                        ex = new Exception("从用户列表中移除用户成功。");
                    }
                    else
                    {
                        ex = new Exception("用户在用户列表中不存在。");
                    }
                }
            }
            else
            {
                ex = new Exception("用户对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnRemoveUser(arg);
        }
    }

    /// <summary>
    /// 住所房间分部类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 房间对象列表
        /// </summary>
        public List<Room> rooms { get; private set; }

        /// <summary>
        /// 处理添加房间的事件
        /// </summary>
        public event EventHandler AddRoomCompleted;

        /// <summary>
        /// 处理移除房间的事件
        /// </summary>
        public event EventHandler RemoveRoomCompleted;

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
        /// 添加房间
        /// </summary>
        /// <param name="name">房间名称</param>
        public void AddRoom(string name)
        {
            if (!string.IsNullOrEmpty(name) && "all" != name.Trim())
            {
                Room room = new Room(name.Trim());
                this.AddRoom(room);
                return;
            }

            Exception ex = new Exception("房间名称不能为" + (string.IsNullOrEmpty(name) ? "空" : "all") + "。");
            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddRoom(arg);
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
                        //if (rooms.IndexOf(room) >= 0)
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

        /// <summary>
        /// 获取代表所有房间的特殊房间
        /// </summary>
        /// <returns></returns>
        public Room GetSpecialRoom()
        {
            Room room = null;

            if (rooms == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                foreach (var rm in rooms)
                {
                    if (rm.Name == "all")
                    {
                        room = rm;
                        break;
                    }
                }
            }

            return room;
        }
    }

    /// <summary>
    /// 住所区域分部类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 区域对象列表
        /// </summary>
        public List<Zone> zones { get; private set; }

        /// <summary>
        /// 处理添加区域的事件
        /// </summary>
        public event EventHandler AddZoneCompleted;

        /// <summary>
        /// 处理移除区域的事件
        /// </summary>
        public event EventHandler RemoveZoneCompleted;

        /// <summary>
        /// 引发处理添加区域的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAddZone(UpdateEventArgs e)
        {
            if (this.AddZoneCompleted != null)
            {
                this.AddZoneCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理移除区域的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnRemoveZone(UpdateEventArgs e)
        {
            if (this.RemoveZoneCompleted != null)
            {
                this.RemoveZoneCompleted(this, e);
            }
        }

        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="name">区域名称</param>
        public void AddZone(string name)
        {
            if (!string.IsNullOrEmpty(name) && "all" != name.Trim())
            {
                Zone zone = new Zone(name.Trim());
                this.AddZone(zone);
                return;
            }

            Exception ex = new Exception("区域名称不能为" + (string.IsNullOrEmpty(name) ? "空" : "all") + "。");
            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddZone(arg);
        }

        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="zone">区域对象</param>
        public void AddZone(Zone zone)
        {
            Exception ex = null;

            if (zone != null)
            {
                if (!string.IsNullOrEmpty(zone.Name) && "all" != zone.Name.Trim())
                {
                    if (zones == null)
                    {
                        ex = new Exception("区域列表重新初始化，添加区域成功。");
                        zones = new List<Zone>();
                        zones.Add(zone);
                    }
                    else
                    {
                        if (zones.Contains(zone))
                        {
                            ex = new Exception("区域已经存在于区域列表中。");
                        }
                        else
                        {
                            ex = new Exception("添加区域成功。");
                            zones.Add(zone);
                        }
                    }
                }
                else
                {
                    ex = new Exception("区域名称不能为" + (string.IsNullOrEmpty(zone.Name) ? "空" : "all") + "。");
                }
            }
            else
            {
                ex = new Exception("区域对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddZone(arg);
        }

        /// <summary>
        /// 移除区域
        /// </summary>
        /// <param name="zone">区域对象</param>
        public void RemoveZone(Zone zone)
        {
            Exception ex = null;

            if (zone != null)
            {
                if (!string.IsNullOrEmpty(zone.Name) && "all" != zone.Name.Trim())
                {
                    if (rooms == null)
                    {
                        ex = new Exception("区域列表不存在，已将其重新初始化。");
                        rooms = new List<Room>();
                    }
                    else
                    {
                        if (zones.Remove(zone))
                        {
                            ex = new Exception("从区域列表中移除区域成功。");
                        }
                        else
                        {
                            ex = new Exception("区域在区域列表中不存在。");
                        }
                    }
                }
                else
                {
                    ex = new Exception("区域名称不能为" + (string.IsNullOrEmpty(zone.Name) ? "空" : "all") + "。");
                }
            }
            else
            {
                ex = new Exception("区域对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnRemoveZone(arg);
        }
    }
}